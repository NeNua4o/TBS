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
    class UnitsRepository : IRepository<Unit>
    {
        string _dataPath;
        List<Unit> _units = new List<Unit>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public UnitsRepository(string dataPath)
        {
            _dataPath = Path.Combine(Application.StartupPath, dataPath);
        }

        ~UnitsRepository()
        {
            _units = null;
        }

        public List<Unit> GetItems()
        {
            return _units;
        }

        public void LoadItems()
        {
            _units.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Unit>));
                _units = (List<Unit>)s.Deserialize(fs);

                for (int i = 0; i < _units.Count; i++)
                {
                    if (!String.IsNullOrEmpty(_units[i].IconPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(_units[i].IconPath);
                            tmpImage.Tag = _units[i].IconPath;
                            _units[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            _units[i].IconPath = "";
                        }
                    }

                    if (!String.IsNullOrEmpty(_units[i].ModelPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(_units[i].ModelPath);
                            tmpImage.Tag = _units[i].ModelPath;
                            _units[i].Model = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            _units[i].ModelPath = "";
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
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].MainActId != -1)
                    _units[i].MainAct = actions.FirstOrDefault(action => action.Id == _units[i].MainActId);
                if (_units[i].SecondActId != -1)
                    _units[i].SecondAct = actions.FirstOrDefault(action => action.Id == _units[i].SecondActId);

                if (_units[i].SkillsIds != null)
                {
                    _units[i].Skills = new Act[_units[i].SkillsIds.Length];
                    for (int j = 0; j < _units[i].SkillsIds.Length; j++)
                        _units[i].Skills[j] = actions.FirstOrDefault(action => action.Id == _units[i].SkillsIds[j]);
                }

                if (_units[i].SpellsIds != null)
                {
                    _units[i].Spells = new Act[_units[i].SpellsIds.Length];
                    for (int j = 0; j < _units[i].SpellsIds.Length; j++)
                        _units[i].Spells[j] = actions.FirstOrDefault(action => action.Id == _units[i].SpellsIds[j]);
                }

                if (_units[i].PassivesIds != null)
                {
                    _units[i].Passives = new Act[_units[i].PassivesIds.Length];
                    for (int j = 0; j < _units[i].PassivesIds.Length; j++)
                        _units[i].Passives[j] = actions.FirstOrDefault(action => action.Id == _units[i].PassivesIds[j]);
                }
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
                s.Serialize(fs, _units);
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
            _units.Add(newUnit);
            return newUnit;
        }

        public void RemoveItem(Unit item)
        {
            _units.Remove(item);
        }

        private int GenerateUnitId()
        {
            int id = 0;
            int[] existedIds = _units.Select(x => x.BaseId).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }
    }
}
