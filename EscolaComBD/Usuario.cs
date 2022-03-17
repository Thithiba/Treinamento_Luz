using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetoEscola
{
    public class Usuario : ICloneable
    {
        private string tipo;
        private string name;
        private string rg;
        private string cpf;
        private int id;
        private string data_nasc;

        //Criar o construtor sempre. Ele diz para a pessoa que está usando o meu código
        //como estou utilizando as classes que eu crio
        public Usuario()
        {

        }

        public Usuario(string tipo, string name, string rg, string cpf, string id, string data_nasc)
        {
            this.tipo = tipo;
            this.name = name;
            this.rg = rg;
            this.cpf = cpf;
            this.data_nasc = data_nasc;
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Data  
        {
            get { return data_nasc; }
            set { data_nasc = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
