using System.Collections.Generic;

namespace Model.Entity.StateMashinePac
{
    public class StateMashine
    {
        public List<State> States = new List<State>();

        private State _currentState;

        public State CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                _currentState.Action();
            }
        } 
    }
}