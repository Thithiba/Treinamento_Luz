using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Npgsql;

namespace ProjetoEscola
{
    public class MainWindowsVM
    {
        //Isso é uma variável que sabe manipular objetos na memória/Ele não é
        //necessariamente um objeto
        public List<Usuario> listaUsuarios { get; set; }
        public ObservableCollection<Usuario> listaFiltrada { get; set; }

        public ICommand Edit { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Filtrar { get; private set; }

        public Usuario UsuarioSelecionado { get; set; }
        public string dadoTipoCreate { get; set; }
        public BDAluno bd_Aluno;
        public BDProfessor bd_Professor;
        public BDUsuario bd_Usuario;

        

        //A vantagem de usar no construtor ele garante que só vai ser chamando uma vez
        public MainWindowsVM()
        {
            listaUsuarios = new List<Usuario>();
            bd_Usuario = new BDUsuario();
            bd_Aluno = new BDAluno();
            bd_Professor = new BDProfessor();
            listaFiltrada = new ObservableCollection<Usuario>(bd_Usuario.GetTodosRegistrosUsuario(listaUsuarios, bd_Aluno, bd_Professor));

            IniciaComandos();
        }

        public void IniciaComandos()
        {
            int userID;
            Add = new RelayCommand((object _) =>
            {
                switch (dadoTipoCreate) 
                {
                    case "Aluno":
                        Aluno userCadastro = new Aluno();
                        CadastroUsuario tela = new CadastroUsuario();
                        tela.DataContext = userCadastro;
                        tela.ShowDialog();

                        if (tela.DialogResult == true)
                        {
                            userCadastro.Tipo = "Aluno";
                            userID = bd_Usuario.InsereUsuario(userCadastro.Tipo,userCadastro.Name, userCadastro.Rg, userCadastro.Cpf, userCadastro.Data);
                            bd_Aluno.InsereAluno(userID, userCadastro.NamePai, userCadastro.TelPai, userCadastro.NameMae, userCadastro.TelMae, userCadastro.Turma);
                            listaFiltrada.Add(userCadastro);
                            //listaUsuarios.Add(userCadastro);
                        }
                        break;

                    case "Professor":
                        Professor profCadastro = new Professor();
                        CadastroProfessor telin = new CadastroProfessor();
                        telin.DataContext = profCadastro;
                        telin.ShowDialog();

                        if (telin.DialogResult == true)
                        {
                            profCadastro.Tipo = "Professor";
                            userID = bd_Usuario.InsereUsuario(profCadastro.Tipo, profCadastro.Name, profCadastro.Rg, profCadastro.Cpf, profCadastro.Data);
                            bd_Professor.InsereProfessor(userID, profCadastro.Contato, profCadastro.TelContato, profCadastro.Email, profCadastro.Materia);
                            listaFiltrada.Add(profCadastro);
                            //listaUsuarios.Add(profCadastro);
                        }
                        break;
                }
        

        //Esse if e else servem para não salvar as informações colocadas no add SE não for clicado em salvar 
        //ao clicar em salvar ele vai receber o true e assim ele adicionara o listausuarios.add

    }, (object _) =>
            {
                //Palavra mágica *plim* (não é uma boa prática)
                return dadoTipoCreate != "Todos";
            });
            Remove = new RelayCommand((object _) =>
            {
                if (UsuarioSelecionado != null)
                {
                    switch (UsuarioSelecionado.Tipo)
                    {
                        case "Aluno":
                            Aluno alunoCadastro = (Aluno)UsuarioSelecionado;
                            bd_Aluno.DeletaAluno(alunoCadastro.Aluno_Id);
                            bd_Usuario.DeletaUsuario(UsuarioSelecionado.Id);
                            break;

                        case "Professor":
                            Professor profCadastro = (Professor)UsuarioSelecionado;
                            bd_Professor.DeletaProfessor(profCadastro.Prof_Id);
                            bd_Usuario.DeletaUsuario(UsuarioSelecionado.Id);
                            break;
                    }
                    listaUsuarios.Remove(UsuarioSelecionado);
                    listaFiltrada.Remove(UsuarioSelecionado);
                }

            }, (object _) =>
            {
                return listaFiltrada.Count != 0;    
            });
            Edit = new RelayCommand((object _) => 
            {
                switch (UsuarioSelecionado.Tipo)
                {
                    case "Aluno":
                        if (UsuarioSelecionado != null)
                        {
                            Aluno alunoCadastro = (Aluno)UsuarioSelecionado;
                            Aluno clone = (Aluno)UsuarioSelecionado.Clone();
                            CadastroUsuario tela = new CadastroUsuario();
                            tela.DataContext = clone;
                            tela.ShowDialog();

                            if (tela.DialogResult == true)
                            {
                                int index = listaFiltrada.IndexOf(alunoCadastro);
                                listaFiltrada.RemoveAt(index);
                                listaFiltrada.Insert(index, clone);
                                bd_Usuario.AtualizaUsuario(clone.Id, clone.Name, clone.Rg, clone.Cpf, clone.Data);
                                bd_Aluno.AtualizaAluno(clone.Aluno_Id, clone.NamePai, clone.TelPai, clone.NameMae, clone.TelMae, clone.Turma);
                            }
                        }
                        break;

                    case "Professor":
                        if (UsuarioSelecionado != null)
                        {
                            Professor profCadastro = (Professor)UsuarioSelecionado;
                            Professor clone = (Professor)UsuarioSelecionado.Clone();
                            CadastroProfessor tela = new CadastroProfessor();
                            tela.DataContext = clone;
                            tela.ShowDialog();

                            if (tela.DialogResult == true)
                            {
                                int index = listaFiltrada.IndexOf(profCadastro);
                                listaFiltrada.RemoveAt(index);
                                listaFiltrada.Insert(index, clone);
                                bd_Usuario.AtualizaUsuario(clone.Id, clone.Name, clone.Rg, clone.Cpf, clone.Data);
                                bd_Professor.AtualizaProfessor(clone.Prof_Id, clone.Contato, clone.TelContato, clone.Email, clone.Materia);
                            }
                        }
                        break;
                }

            }, (object _) =>
            {
                //Palavra mágica *plim* (não é uma boa prática)
                return dadoTipoCreate != "Todos";
            });

            Filtrar = new RelayCommand((object _) =>
            {
                for (int i = listaFiltrada.Count - 1; i >= 0; i--)
                {
                    listaFiltrada.Remove(listaFiltrada[i]);
                }

                if (dadoTipoCreate == "Todos" || dadoTipoCreate == null)
                    for (int i = 0; i < listaUsuarios.Count; i++)
                        listaFiltrada.Add(listaUsuarios[i]);
                else
                    for (int i = 0; i < listaUsuarios.Count; i++)
                        if (listaUsuarios[i].Tipo == dadoTipoCreate)
                            listaFiltrada.Add(listaUsuarios[i]);
            });
        }
    }
}
