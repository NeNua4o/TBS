using NLog;
using System.Collections.Generic;

namespace Common.Repositories
{
    public class RepositoryWorker
    {
        public List<BaseUnit> BaseUnits { get { return _baseUnitRepository.GetItems(); } }
        public List<Act> Actions { get { return _actionRepository.GetItems(); } }
        public List<Effect> Effects = new List<Effect>();

        private static RepositoryWorker _instance;
        private BaseUnitsRepository _baseUnitRepository;
        private ActionsRepository _actionRepository;
        private EffectsRepository _effectRepository;

        Logger _logger = LogManager.GetCurrentClassLogger();

        private RepositoryWorker()
        {
            _baseUnitRepository = new BaseUnitsRepository("BaseUnits.xml");
            _actionRepository = new ActionsRepository("Actions.xml");
            _effectRepository = new EffectsRepository("Effects.xml");
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
        }

        public void SaveAll()
        {
            _effectRepository.LoadItems();
            _actionRepository.SaveItems();
            _baseUnitRepository.SaveItems();
        }

        public BaseUnit CreateBaseUnit() { return _baseUnitRepository.CreateItem(); }
        public void DeleteBaseUnit(BaseUnit unit) { _baseUnitRepository.RemoveItem(unit); }

        public Act CreateAction() { return _actionRepository.CreateItem(); }
        public void DeleteAction(Act action) { _actionRepository.RemoveItem(action); }

        public Effect CreateEffect(){ return _effectRepository.CreateItem(); }
        public void DeleteEffect(Effect effect){ _effectRepository.RemoveItem(effect); }
    }
}
