using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Common.Repositories
{
    public class EffectsRepository : IRepository<Effect>
    {
        string _dataPath;
        List<Effect> _effects = new List<Effect>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public EffectsRepository(string dataPath)
        {
            _dataPath = dataPath;
        }

        ~EffectsRepository()
        {
            _effects = null;
        }

        public List<Effect> GetItems()
        {
            return _effects;
        }

        public void LoadItems()
        {
            _effects.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(_dataPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Effect>));
                _effects = (List<Effect>)s.Deserialize(fs);

                for (int i = 0; i < _effects.Count; i++)
                {
                    if (!String.IsNullOrEmpty(_effects[i].IconPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(_effects[i].IconPath);
                            tmpImage.Tag = _effects[i].IconPath;
                            _effects[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            _effects[i].IconPath = "";
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
                s = new XmlSerializer(typeof(List<Effect>));
                s.Serialize(fs, _effects);
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

        public Effect CreateItem()
        {
            var newEffect = new Effect();
            newEffect.Id = GenerateEffectId();
            _effects.Add(newEffect);
            return newEffect;
        }

        public void RemoveItem(Effect item)
        {
            _effects.Remove(item);
        }

        private int GenerateEffectId()
        {
            int id = 0;
            int[] existedIds = _effects.Select(x => x.Id).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }
    }
}
