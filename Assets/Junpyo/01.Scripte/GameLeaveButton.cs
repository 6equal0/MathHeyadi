using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLeaveButton : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        // �����Ϳ��̿���
        Debug.Log("���� ����");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ������̿���
            Application.Quit();
#endif
    }
}
