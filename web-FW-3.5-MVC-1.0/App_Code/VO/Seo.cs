using System;
using System.Text;

namespace VO
{

    public class Seo
    {
        private int _id;
        private string _url;
        private string _h1;
        private string _description;
        private string _keywords;
        private string _title;
        private string _breadcrumb;
        private VO.SEOChangeFreq _change_freq;
        private string _prioridade;
        private DateTime _data_cadastro;
        private bool _status;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Url
        {
            get { return this._url; }
            set { this._url = value; }
        }

        public string H1
        {
            get { return this._h1; }
            set { this._h1 = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public string Keywords
        {
            get { return this._keywords; }
            set { this._keywords = value; }
        }

        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public VO.SEOChangeFreq Change_freq
        {
            get { return this._change_freq == null ? this._change_freq = new VO.SEOChangeFreq() : this._change_freq; }
            set { this._change_freq = value; }
        }

        public string Prioridade
        {
            get { return this._prioridade; }
            set { this._prioridade = value; }
        }

        public bool Status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        public string Breadcrumb
        {
            get { return this._breadcrumb; }
            set { this._breadcrumb = value; }
        }
        public DateTime Data_cadastro
        {
            get { return this._data_cadastro; }
            set { this._data_cadastro = value; }
        }

        public Seo()
        {
        }

        public Seo(string url)
        {
            this._url = url;
        }

        public Seo(string url, string breadcrumb)
        {
            this._url = url;
            this._breadcrumb = breadcrumb;
        }

    }

}