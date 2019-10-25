using Common.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Common.Repositories
{
    public class ActionsRepository : IRepository<Act>
    {
        string _dataPath;
        List<Act> _actions = new List<Act>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionsRepository(string dataPath)
        {
            _dataPath = dataPath;
        }

        ~ActionsRepository()
        {
            _actions = null;
        }

        public List<Act> GetItems()
        {
            return _actions;
        }

        public void LoadItems()
        {
            _actions.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Act>));
                _actions = (List<Act>)s.Deserialize(fs);

                for (int i = 0; i < _actions.Count; i++)
                {
                    if (!String.IsNullOrEmpty(_actions[i].IconPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(_actions[i].IconPath);
                            tmpImage.Tag = _actions[i].IconPath;
                            _actions[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            _actions[i].IconPath = "";
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

        public void SaveItems()
        {
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Create);
                s = new XmlSerializer(typeof(List<Act>));
                s.Serialize(fs, _actions);
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

        public Act CreateItem()
        {
            var newAction = new Act();
            newAction.Id = GenerateActionId();
            _actions.Add(newAction);
            return newAction;
        }

        public void RemoveItem(Act item)
        {
            _actions.Remove(item);
        }

        private int GenerateActionId()
        {
            int id = 0;
            int[] existedIds = _actions.Select(x => x.Id).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }
    }
}
