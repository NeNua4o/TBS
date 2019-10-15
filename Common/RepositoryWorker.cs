using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TBS;

namespace Common
{
    public class RepositoryWorker
    {
        const string BaseUnitsPath = "BaseUnits.xml";
        const string ActionsPath = "Actions.xml";

        public List<BaseUnit> BaseUnits = new List<BaseUnit>();
        public List<Act> Actions = new List<Act>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        public void LoadAll()
        {
            LoadUnits();
            LoadActions();
        }

        public void SaveAll()
        {
            SaveUnits();
        }

        public void LoadUnits()
        {
            BaseUnits.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(BaseUnitsPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<BaseUnit>));
                BaseUnits = (List<BaseUnit>)s.Deserialize(fs);
                
                for (int i = 0; i < BaseUnits.Count; i++)
                {
                    if (!String.IsNullOrEmpty(BaseUnits[i].IconPath))
                    {
                        tmpImage = Image.FromFile(BaseUnits[i].IconPath);
                        tmpImage.Tag = BaseUnits[i].IconPath;
                        BaseUnits[i].Icon = tmpImage;
                    }

                    if (!String.IsNullOrEmpty(BaseUnits[i].ModelPath))
                    {
                        tmpImage = Image.FromFile(BaseUnits[i].ModelPath);
                        tmpImage.Tag = BaseUnits[i].ModelPath;
                        BaseUnits[i].Model = tmpImage;
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

        public void SaveUnits()
        {
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(BaseUnitsPath, FileMode.Create);
                s = new XmlSerializer(typeof(List<BaseUnit>));
                s.Serialize(fs, BaseUnits);
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

        public BaseUnit CreateUnit()
        {
            var newUnit = new BaseUnit();
            newUnit.Id = GenerateUnitId();
            BaseUnits.Add(newUnit);
            return newUnit;
        }

        public void DeleteUnit(BaseUnit unit)
        {
            BaseUnits.Remove(unit);
        }

        private int GenerateUnitId()
        {
            int id = 0;
            int[] existedIds = BaseUnits.Select(x => x.Id).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }





        public void LoadActions()
        {
            Actions.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(ActionsPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Act>));
                Actions = (List<Act>)s.Deserialize(fs);

                for (int i = 0; i < Actions.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Actions[i].IconPath))
                    {
                        tmpImage = Image.FromFile(Actions[i].IconPath);
                        tmpImage.Tag = Actions[i].IconPath;
                        Actions[i].Icon = tmpImage;
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

        public void SaveActions()
        {
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(ActionsPath, FileMode.Create);
                s = new XmlSerializer(typeof(List<Act>));
                s.Serialize(fs, Actions);
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

        public Act CreateAction()
        {
            var newAction = new Act();
            newAction.Id = GenerateActionId();
            Actions.Add(newAction);
            return newAction;
        }

        public void DeleteAction(Act action)
        {
            Actions.Remove(action);
        }

        private int GenerateActionId()
        {
            int id = 0;
            int[] existedIds = Actions.Select(x => x.Id).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }
    }
}
