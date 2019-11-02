using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patrol;

namespace Patrol{
    public class SSDirector : System.Object {

        private static SSDirector _instance;
        public ISceneController currentSceneController { get; set; }

        SSDirector() { }

        public static SSDirector getInstance()
        {
            if (_instance == null)
                _instance = new SSDirector();
            return _instance;
        }

        public int getFPS()
        {
            return Application.targetFrameRate;
        }

        public void setFPS(int fps)
        {
            Application.targetFrameRate = fps;
        }
        public void NextScene()
        {

        }
    }

    public interface ISceneController {
        void GenGameObjects();
    }

    public enum SSActionEventType:int { Started, Completed}

    public interface ISSActionCallback {
        void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, 
                            int intParam = 0, string strParam = null, Object objParam = null);
        void CheckEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, 
                            int intParam = 0, string strParam = null, Object objParam = null);
    }

    public interface IUserAction {
        void Restart();
        void Pause();
    }


}
