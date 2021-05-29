using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;

namespace MVC.DAL
{
    public class ParserWorker<T> 
    {
        IParser<T> parser;
        IParserSettings parserSettings;
        string endUrl; 

        HtmlLoader loader;

        public bool isActive;

        public bool isEmpty;

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings ParserSettings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings)
        {
            this.parser = parser;
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Terminate()
        {
            isActive = false;
        }

        private async void Worker()
        {

            for (int i = parserSettings.StartPoint; i < parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPage(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }
            
            OnCompleted?.Invoke(this);
            isActive = false;
        }


    }
}
