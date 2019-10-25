using NLog;
using System.Collections.Generic;
using System;
using Common.Models;

namespace Common.Repositories
{
    public class RepositoryWorker
    {
        public List<BaseUnit> BaseUnits { get { return _baseUnitRepository.GetItems(); } }
        public List<Act> Actions { get { return _actionRepository.GetItems(); } }
        public List<Effect> Effects { get { return _effectRepository.GetItems(); } }
        public List<Pl> Pls { get { return _plRepository.GetItems(); } }
        public List<Team> Teams { get { return _teamRepository.GetItems(); } }

        
        private static RepositoryWorker _instance;
        private BaseUnitsRepository _baseUnitRepository;
        private ActionsRepository _actionRepository;
        private EffectsRepository _effectRepository;
        private PlsRepository _plRepository;
        private TeamRepository _teamRepository;

        Logger _logger = LogManager.GetCurrentClassLogger();

        private RepositoryWorker()
        {
            _baseUnitRepository = new BaseUnitsRepository("BaseUnits.xml");
            _actionRepository = new ActionsRepository("Actions.xml");
            _effectRepository = new EffectsRepository("Effects.xml");
            _plRepository = new PlsRepository("Pls.xml");
            _teamRepository = new TeamRepository();
        }

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
            _effectRepository.LoadItems();
            _actionRepository.LoadItems();
            _baseUnitRepository.LoadItems();
            _plRepository.LoadItems();
            _plRepository.UpdateUnits(_baseUnitRepository.GetItems());
        }

        public void SaveAll()
        {
            _effectRepository.SaveItems();
            _actionRepository.SaveItems();
            _baseUnitRepository.SaveItems();
            _plRepository.SaveItems();
        }

        public BaseUnit CreateBaseUnit() { return _baseUnitRepository.CreateItem(); }
        public void DeleteBaseUnit(BaseUnit unit) { _baseUnitRepository.RemoveItem(unit); }

        public Act CreateAction() { return _actionRepository.CreateItem(); }
        public void DeleteAction(Act action) { _actionRepository.RemoveItem(action); }

        public Effect CreateEffect() { return _effectRepository.CreateItem(); }
        public void DeleteEffect(Effect effect) { _effectRepository.RemoveItem(effect); }

        public Pl CreatePl() { return _plRepository.CreateItem(); }

        public void DeletePl(Pl pl) { _plRepository.RemoveItem(pl); }




        public BaseUnit GetBaseUnit(int id)
        {
            var bUnits = _baseUnitRepository.GetItems();
            for (int i = 0; i < bUnits.Count; i++)
            {
                if (bUnits[i].Id == id)
                    return bUnits[i];
            }
            return null;
        }

        public Act GetAction(int id)
        {
            var actions = _actionRepository.GetItems();
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].Id == id)
                    return actions[i];
            }
            return null;
        }

        public Team GetTeam(int id)
        {
            var teams = _teamRepository.GetItems();
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Id == id)
                    return teams[i];
            }
            return null;
        }

    }
}
