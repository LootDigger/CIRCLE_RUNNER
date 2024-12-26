using AUL.Core;

namespace AUL.Analytic
{
    public class TravelDistanceSpyService : IDistanceAnalyticService
    {
        private IGameModel _model;
        
        public TravelDistanceSpyService(IGameModel model)
        {
            _model = model;
        }
        
        public void RegisterNewDeltaDistance(float deltaDistance)
        {
            _model.UpdateDistanceModel(deltaDistance);
        }
    }
}