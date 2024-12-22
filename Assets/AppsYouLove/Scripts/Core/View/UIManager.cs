using TMPro;
using UnityEngine;

namespace AUL.Core
{
    public class UIManager : MonoBehaviour, IView
    {
        [SerializeField] 
        private TextMeshProUGUI _scoreText;
        
        [SerializeField] 
        private TextMeshProUGUI _distanceText;
        
        
        public void UpdateScoreView(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void UpdateDistanceTrackerView(float distance)
        {
            _distanceText.text = distance.ToString();
        }
    }
}