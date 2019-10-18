using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Common
{
    public class RepositoryWorker
    {
        const string BaseUnitsPath = "BaseUnits.xml";
        const string ActionsPath = "Actions.xml";
        const string EffectsPath = "Effects.xml";

        public List<BaseUnit> BaseUnits = new List<BaseUnit>();
        public List<Act> Actions = new List<Act>();
        public List<Effect> Effects = new List<Effect>();

        Logger _logger = LogManager.GetCurrentClassLogger();

        private static RepositoryWorker _instance;

        private RepositoryWorker() { }

        public static RepositoryWorker GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RepositoryWorker();
                _instance.LoadAll();
            }
            return _instance;
        }

        public void LoadAll()
        {
            LoadEffects();
            LoadActions();
            LoadUnits();
        }

        public void SaveAll()
        {
            SaveEffects();
            SaveActions();
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
                        try
                        {
                            tmpImage = Image.FromFile(BaseUnits[i].IconPath);
                            tmpImage.Tag = BaseUnits[i].IconPath;
                            BaseUnits[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            BaseUnits[i].IconPath = "";
                        }
                    }

                    if (!String.IsNullOrEmpty(BaseUnits[i].ModelPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(BaseUnits[i].ModelPath);
                            tmpImage.Tag = BaseUnits[i].ModelPath;
                            BaseUnits[i].Model = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            BaseUnits[i].ModelPath = "";
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
                        try
                        {
                            tmpImage = Image.FromFile(Actions[i].IconPath);
                            tmpImage.Tag = Actions[i].IconPath;
                            Actions[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            Actions[i].IconPath = "";
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



        public void LoadEffects()
        {
            Effects.Clear();
            FileStream fs = null;
            XmlSerializer s;
            Image tmpImage = null;
            try
            {
                fs = new FileStream(EffectsPath, FileMode.Open);
                s = new XmlSerializer(typeof(List<Effect>));
                Effects = (List<Effect>)s.Deserialize(fs);

                for (int i = 0; i < Effects.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Effects[i].IconPath))
                    {
                        try
                        {
                            tmpImage = Image.FromFile(Effects[i].IconPath);
                            tmpImage.Tag = Effects[i].IconPath;
                            Effects[i].Icon = tmpImage;
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, ex.Message);
                            Effects[i].IconPath = "";
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

        public void SaveEffects()
        {
            FileStream fs = null;
            XmlSerializer s;
            try
            {
                fs = new FileStream(EffectsPath, FileMode.Create);
                s = new XmlSerializer(typeof(List<Effect>));
                s.Serialize(fs, Effects);
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

        public Effect CreateEffect()
        {
            var newEffect = new Effect();
            newEffect.Id = GenerateEffectId();
            Effects.Add(newEffect);
            return newEffect;
        }

        public void DeleteEffect(Effect effect)
        {
            Effects.Remove(effect);
        }

        private int GenerateEffectId()
        {
            int id = 0;
            int[] existedIds = Effects.Select(x => x.Id).ToArray();
            while (existedIds.Contains(id)) { id++; }
            existedIds = null;
            return id;
        }







        public Effect GetEffectById(int id)
        {
            for (int i = 0; i < Effects.Count; i++)
                if (Effects[i].Id == id)
                    return Effects[i];
            return null;
        }




    }
}
