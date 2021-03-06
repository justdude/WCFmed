﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedClient.ServiceReference1;
using System.Windows.Data;
using System.ServiceModel;

namespace MedClient
{
    class Client
    {
        //JsonFxAdapter<Connector>.LoadJson()

        private static Client _client = null;
        public static Client ClientInstance
        {
            get
            {
                if (_client == null) _client = new Client();
                return _client;
            }
        }

        private MedWCFClient _medClient = null;
        public MedWCFClient MedClient
        {
            get
            {
                if (_medClient == null)
                {
                    BasicHttpBinding binding = new BasicHttpBinding();
                    EndpointAddress adress = new EndpointAddress(Path);
                    _medClient = new MedWCFClient(binding,adress);

                }
                return _medClient;
            }
        }

        private static string _path = @"http://localhost:8085/MedWCF.svc";
        public static string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        public bool Logged
        {
            get;
            private set;
        }

        public peoples info = null;
        
        public bool Login(string name, string pass)
        {
            if (MedClient.Login(name, pass) == "OK")
                Logged = true;
            else
                Logged = false;

            info = this.MedClient.GetPeopleInfo(name);

            return Logged;
        }





        ~Client()
        {
            _medClient.Close();
        }


    }
}
