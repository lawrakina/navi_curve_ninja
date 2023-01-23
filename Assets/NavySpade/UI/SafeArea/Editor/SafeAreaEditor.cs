using UnityEditor;
using UnityEngine;

namespace NavySpade.UI.SafeArea.Editor
{
	[CustomEditor(typeof(SafeArea))]
	public partial class SafeAreaEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying) return;

			var controller = SafeAreaController.Instance;
			if (controller == null) return;

			EditorGUILayout.Separator();
			SafeAreaControllerEditor.Draw(controller);
		}
	}
}
