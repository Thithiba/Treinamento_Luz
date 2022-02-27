using System;
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
        public ObservableCollection<Aluno> listaUsuarios { get; set; }

        public ICommand Edit { get; private set; }

        public ICommand Add { get; private set; }

        public ICommand Remove { get; private set; }

        public int index { get; set; }

        public Aluno UsuarioSelecionado { get; set; }

        //A vantagem de usar no construtor ele garante que só vai ser chamando uma vez
        public MainWindowsVM()
        {
            listaUsuarios = new ObservableCollection<Aluno>()
            {
                new Aluno()
                {
                    Name = "Eduardo",
                    Data = "23/05/1997",
                    Rg = "65465465",
                    Cpf = "321321",
                    NamePai = "Pai",
                    TelPai = "1111-1111",
                    NameMae = "2222-2222",
                    TelMae = "Mae",
                    Turma = "2B"
                }
            };
            IniciaComandos();
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {

                Aluno userCadastro = new Aluno();
                CadastroUsuario tela = new CadastroUsuario();
                tela.DataContext = userCadastro;
                tela.ShowDialog();

                if(tela.DialogResult == true)
                {
                    listaUsuarios.Add(userCadastro);
                }
                    //Esse if e else servem para não salvar as informações colocadas no add SE não for clicado em salvar 
                    //ao clicar em salvar ele vai receber o true e assim ele adicionara o listausuarios.add
                
            });
            Remove = new RelayCommand((object _) =>
            {
                if (UsuarioSelecionado != null)
                {
                    listaUsuarios.Remove(UsuarioSelecionado);
                }
               
            }, (object _) =>
            {
                return listaUsuarios.Count != 0;    
            });
            Edit = new RelayCommand((object _) => {

                if (UsuarioSelecionado != null)
                {

                    Aluno userCadastro = UsuarioSelecionado;
                    Aluno novo = (Aluno)UsuarioSelecionado.Clone();
                    CadastroUsuario tela = new CadastroUsuario();
                    tela.DataContext = novo;
                    tela.ShowDialog();
                    


                    if (tela.DialogResult == true)
                    {
                        int index = listaUsuarios.IndexOf(userCadastro);
                        listaUsuarios.RemoveAt(index);
                        listaUsuarios.Insert(index, novo);
                    }
                }
            });
        }
    }
}
