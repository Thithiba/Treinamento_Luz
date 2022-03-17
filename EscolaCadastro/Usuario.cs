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
        private string id;
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

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Data  
        {
            get { return data_nasc; }
            set { data_nasc = value; }
        }

        public void ParseExpressaoRegular(string stringPosicional)
        {
            Regex regex = new Regex(@"(?<Nome>.{0,50})(?<DataNascimento>\d{8})(?<Cpf>\d{11})(?<Rg>\d{9})");

            Match match = regex.Match(stringPosicional);

            if (match.Success)
            {
                Name = match.Groups["Nome"].Value;
                Data = match.Groups["DataNascimento"].Value;
                Cpf = match.Groups["Cpf"].Value;
                Rg = match.Groups["Rg"].Value;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
