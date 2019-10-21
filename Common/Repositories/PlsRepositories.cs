using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Common.Repositories
{
    public class PlsRepositories : IRepository<Pl>
    {
        string _dataPath;
        List<Pl> _pls = new List<Pl>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public PlsRepositories(string dataPath)
        {
            _dataPath = dataPath;
        }

        public List<Pl> GetItems()
        {
            return _pls;
        }

        public void LoadItems()
        {
            throw new NotImplementedException();
        }

        public void SaveItems()
        {
            throw new NotImplementedException();
        }

        public Pl CreateItem()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(Pl item)
        {
            throw new NotImplementedException();
        }

        
    }
}
