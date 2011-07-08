using System;
using System.Text;

namespace VO
{
    public class SEOChangeFreq
    {

        private int _id;
        private string _nome;
        private bool _status;
        private DateTime _data_cadastro;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string Nome
        {
            get { return this._nome; }
            set { this._nome = value; }
        }
        public bool Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        public DateTime Data_cadastro
        {
            get { return this._data_cadastro; }
            set { this._data_cadastro = value; }
        }

        public SEOChangeFreq()
        {
        }

        public SEOChangeFreq(string nome)
        {
            this._nome = nome;
        }

    }

}