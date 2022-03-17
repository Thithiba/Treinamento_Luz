using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEscola
{
    public class Professor : Usuario
    {
        private string contato;
        private string telContato;
        private string email;
        private string materia;
        private int prof_id;

        public Professor()
        {

        }

        public Professor(string contato, string telContato, string email, string materia)
        {
            this.contato = contato;
            this.telContato = telContato;
            this.email = email;
            this.materia = materia;
        }

        public string Contato
        {
            get { return contato; }
            set { contato = value; }
        }

        public string TelContato
        {
            get { return telContato; }
            set { telContato = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Materia
        {
            get { return materia; }
            set { materia = value; }
        }

        public int Prof_Id
        {
            get { return prof_id; }
            set { prof_id = value; }
        }

    }
}
