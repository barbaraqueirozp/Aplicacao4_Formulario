using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao4_Formulario
{
    public class Alunos
    {
        // Atributos privados
        private int matricula;
        private string nome;
        private double n1;
        private double n2;
        private double media;
        private string status;

        // Construtores
        public Alunos()
        {
        }

        public Alunos(int matricula, string nome, double n1, double n2, double media, string status)
        {
            this.matricula = matricula;
            this.nome = nome;
            this.n1 = n1;
            this.n2 = n2;
            this.media = media;
            this.status = status;
        }

        // Getters e Setters (Propriedades)
        public int Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public double N1
        {
            get { return n1; }
            set { n1 = value; }
        }

        public double N2
        {
            get { return n2; }
            set { n2 = value; }
        }

        public double Media
        {
            get { return media; }
            set { media = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}