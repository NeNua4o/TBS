using Common.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Common.Repositories
{
    class BaseUnitsRepository : IRepository<Unit>
    {
        string _dataPath;
        List<Unit> _baseUnits = new List<Unit>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public BaseUnitsRepository(string dataPath)
        {
            _dataPath = Path.Combine(Application.StartupPath, dataPath);
        }

        ~BaseUnitsRepository()
        {
            _baseUnits = null;
        }

        public List<Unit> GetItems()
        {
            return _baseUnits;
        }

        public void LoadItems()
        {
            _baseUnits.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Unit>));
                _baseUnits = (List<Unit>)s.Deserialize(fs);

                for (int i = 0; i < _baseUnits.Count; i++)
                {
                    if (!String.IsNullOrEmpty(_baseUnits[i].IconPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(_baseUnits[i].IconPath);
                            tmpImage.Tag = _baseUnits[i].IconPath;
                            _baseUnits[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            _baseUnits[i].IconPath = "";
                        }
                    }

                    if (!String.IsNullOrEmpty(_baseUnits[i].ModelPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(_baseUnits[i].ModelPath);
                            tmpImage.Tag = _baseUnits[i].ModelPath;
                            _baseUnits[i].Model = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            _baseUnits[i].ModelPath = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return;
            }
            finally
            {
                tmpImage = null;
                s = null;
                if (fs != null)
                    fs.Dispose();
            }
        }

        internal void UpdateActions(List<Act> actions)
        {
            if (actions == null) return;
            for (int i = 0; i < _baseUnits.Count; i++)
            {
                if (_baseUnits[i].MainActId != -1)
                    _baseUnits[i].MainAct = actions.FirstOrDefault(action => action.Id == _baseUnits[i].MainActId);
                if (_baseUnits[i].SecondActId != -1)
                    _baseUnits[i].SecondAct = actions.FirstOrDefault(action => action.Id == _baseUnits[i].SecondActId);
            }
        }

        public void SaveItems()
        {
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Create);
                s = new XmlSerializer(typeof(List<Unit>));
                s.Serialize(fs, _baseUnits);
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

        public Unit CreateItem()
        {
            var newUnit = new Unit();
            newUnit.BaseId = GenerateUnitId();
            _baseUnits.Add(newUnit);
            return newUnit;
        }

        public void RemoveItem(Unit item)
        {
            _baseUnits.Remove(item);
        }

        private int GenerateUnitId()
        {
            int id = 0;
            int[] existedIds = _baseUnits.Select(x => x.BaseId).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }
    }
}
