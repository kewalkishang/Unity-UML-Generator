//using UnityEngine;
//using UnityEditor;
 
//public class TestCustomMenu : EditorWindow, IHasCustomMenu
//{
//    [MenuItem("Window/MyWindow")]
//    private static void ShowWindow()
//    {
//        GetWindow<TestCustomMenu>().Show();
//    }

//    // This interface implementation is automatically called by Unity.
//    void IHasCustomMenu.AddItemsToMenu(GenericMenu menu)
//    {
//        GUIContent content = new GUIContent("My Custom Entry");
//        menu.AddItem(content, false, MyCallback);
//    }

//    private void MyCallback()
//    {
//        Debug.Log("My Callback was called.");
//    }
//}