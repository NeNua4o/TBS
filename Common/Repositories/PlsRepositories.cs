using Common.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Common.Repositories
{
    public class PlsRepository : IRepository<Pl>
    {
        string _dataPath;
        List<Pl> _pls = new List<Pl>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public PlsRepository(string dataPath)
        {
            _dataPath = dataPath;
        }

        ~PlsRepository()
        {
            _dataPath = null;
        }

        public List<Pl> GetItems()
        {
            return _pls;
        }

        public void LoadItems()
        {
            _pls.Clear();
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Pl>));
                _pls = (List<Pl>)s.Deserialize(fs);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return;
            }
            finally
            {
                s = null;
                if (fs != null)
                    fs.Dispose();
            }
        }

        public void SaveItems()
        {
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Create);
                s = new XmlSerializer(typeof(List<Pl>));
                s.Serialize(fs, _pls);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return;
            }
            finally
            {
                s = null;
                if (fs != null)
                    fs.Dispose();
            }
        }

        public void UpdateUnits(List<Unit> bUnits)
        {
            for (int plc = 0; plc < _pls.Count; plc++)
            {
                for (int uc = 0; uc < _pls[plc].Units.Count; uc++)
                {
                    var u = _pls[plc].Units[uc];
                    var bu = bUnits.FirstOrDefault(unit => unit.BaseId == u.BId);
                    u.UpdateStats(bu);
                }
            }
        }

        public Pl CreateItem()
        {
            var newPl = new Pl();
            newPl.Id = GeneratePlId();
            _pls.Add(newPl);
            return newPl;
        }

        public void RemoveItem(Pl item)
        {
            _pls.Remove(item);
        }

        private int GeneratePlId()
        {
            int id = 0;
            int[] existedIds = _pls.Select(x => x.Id).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }
    }
}
