using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjetoEscola
{
    public class MainWindowsVM
    {
        //Isso é uma variável que sabe manipular objetos na memória/Ele não é
        //necessariamente um objeto
        public List<Usuario> listaUsuarios { get; set; }

        public ICommand Edit { get; private set; }

        public ICommand Add { get; private set; }

        public ICommand Remove { get; private set; }

        public ICommand Filtrar { get; private set; }

        public Usuario UsuarioSelecionado { get; set; }

        public string dadoTipoCreate { get; set; }

        public ObservableCollection<Usuario> listaFiltrada { get; set; }

        //A vantagem de usar no construtor ele garante que só vai ser chamando uma vez
        public MainWindowsVM()
        {
            listaUsuarios = new List<Usuario>();

            listaUsuarios.Add(new Aluno()
            {
                Tipo = "Aluno",
                Name = "Eduardo",
                Data = "23/05/1997",
                Rg = "65465465",
                Cpf = "321321",
                NamePai = "Pai",
                TelPai = "1111-1111",
                NameMae = "2222-2222",
                TelMae = "Mae",
                Turma = "2B"
            });

            listaUsuarios.Add(new Professor()
            {
                Tipo = "Professor",
                Name = "Eduardo",
                Data = "23/05/1997",
                Rg = "65465465",
                Cpf = "321321",
                Contato = "Juanito",
                TelContato = "3333-3333",
                Email = "jaunito@juanito.com.br",
                Materia = "Matematica"
            });

            listaFiltrada = new ObservableCollection<Usuario>(listaUsuarios);

            IniciaComandos();
            //listaFiltrada = listaUsuarios.Where(n => n.Tipo == dadoTipoCreate);
        }

        public void IniciaComandos()
        {
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
                            listaFiltrada.Add(userCadastro);
                            listaUsuarios.Add(userCadastro);
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
                            listaFiltrada.Add(profCadastro);
                            listaUsuarios.Add(profCadastro);
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
                            }
                        }
                        break;
                }

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
