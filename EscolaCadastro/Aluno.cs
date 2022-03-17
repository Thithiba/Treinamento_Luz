using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjetoEscola
{
    public class Aluno : Usuario
    {
        private string namepai;
        private string telpai;
        private string namemae;
        private string telmae;
        private string turma;

        public Aluno()
        {

        }

        public Aluno(string namePai, string telPai, string nameMae, string telMae, string turma)
        {
            this.namepai = namePai;
            this.telpai = telPai;
            this.namemae = nameMae;
            this.telmae = telMae;
            this.turma = turma;
        }

        public string NamePai
        {
            get { return namepai; }
            set { namepai = value; }
        }

        public string TelPai
        {
            get { return telpai; }
            set { telpai = value; }
        }

        public string NameMae
        {
            get { return namemae; }
            set { namemae = value; }
        }

        public string TelMae
        {
            get { return telmae; }
            set { telmae = value; }
        }

        public string Turma
        {
            get { return turma; }
            set { turma = value; }
        }
    }
}
