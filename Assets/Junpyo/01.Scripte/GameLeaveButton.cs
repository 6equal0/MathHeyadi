using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLeaveButton : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        // 에디터용이에용
        Debug.Log("게임 종료");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 빌드용이에용
            Application.Quit();
#endif
    }
}
