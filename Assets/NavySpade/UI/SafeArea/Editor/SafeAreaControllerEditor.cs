using UnityEditor;

namespace NavySpade.UI.SafeArea.Editor
{
	[CustomEditor(typeof(SafeAreaController))]
	public partial class SafeAreaControllerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var script = target as SafeAreaController;
			Draw(script);
		}

		public static void Draw(SafeAreaController script)
		{
			EditorGUILayout.LabelField("Runtime Debug Settings", EditorStyles.boldLabel);

			script.EditorSafeZone.Left = EditorGUILayout.FloatField(nameof(script.EditorSafeZone.Left), script.EditorSafeZone.Left);
			script.EditorSafeZone.Right = EditorGUILayout.FloatField(nameof(script.EditorSafeZone.Right), script.EditorSafeZone.Right);
			script.EditorSafeZone.Bottom = EditorGUILayout.FloatField(nameof(script.EditorSafeZone.Bottom), script.EditorSafeZone.Bottom);
			script.EditorSafeZone.Top = EditorGUILayout.FloatField(nameof(script.EditorSafeZone.Top), script.EditorSafeZone.Top);
		}
	}
}
