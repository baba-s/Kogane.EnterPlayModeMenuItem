using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// Enter Play Mode Settings の項目をメニューから変更できるようにするエディタ拡張
    /// </summary>
    [InitializeOnLoad]
    internal static class EnterPlayModeMenuItem
    {
        //==============================================================================
        // 定数
        //==============================================================================
        private const string ITEM_NAME_ROOT = "Kogane/Enter Play Mode/";

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static EnterPlayModeMenuItem()
        {
            EditorApplication.delayCall += () => UpdateChecked();
        }

        /// <summary>
        /// Enter Play Mode Options (Experimental) のオン・オフを切り替えるメニュー
        /// </summary>
        [MenuItem( ITEM_NAME_ROOT + "Enter Play Mode Options" )]
        private static void ChangeOption()
        {
            EditorSettings.enterPlayModeOptionsEnabled =
                !EditorSettings.enterPlayModeOptionsEnabled;
            UpdateChecked();
            RepaintProjectSettingsWindow();
        }

        /// <summary>
        /// Reload Domain のオン・オフを切り替えるメニュー
        /// </summary>
        [MenuItem( ITEM_NAME_ROOT + "Reload Domain" )]
        private static void ChangeReloadDomain()
        {
            EditorSettings.enterPlayModeOptions ^= EnterPlayModeOptions.DisableDomainReload;
            UpdateChecked();
            RepaintProjectSettingsWindow();
        }

        /// <summary>
        /// Reload Scene のオン・オフを切り替えるメニュー
        /// </summary>
        [MenuItem( ITEM_NAME_ROOT + "Reload Scene" )]
        private static void ChangeReloadScene()
        {
            EditorSettings.enterPlayModeOptions ^= EnterPlayModeOptions.DisableSceneReload;
            UpdateChecked();
            RepaintProjectSettingsWindow();
        }

        /// <summary>
        /// Reload Scene のオン・オフを切り替えるメニュー
        /// </summary>
        [MenuItem( ITEM_NAME_ROOT + "Disable Scene Backup" )]
        private static void ChangeDisableSceneBackup()
        {
            EditorSettings.enterPlayModeOptions ^= EnterPlayModeOptions.DisableSceneBackupUnlessDirty;
            UpdateChecked();
            RepaintProjectSettingsWindow();
        }

        /// <summary>
        /// メニューのチェックマークの表示を更新します
        /// </summary>
        private static void UpdateChecked()
        {
            Menu.SetChecked( ITEM_NAME_ROOT + "Enter Play Mode Options", EditorSettings.enterPlayModeOptionsEnabled );
            Menu.SetChecked( ITEM_NAME_ROOT + "Reload Domain", ( EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableDomainReload ) == 0 );
            Menu.SetChecked( ITEM_NAME_ROOT + "Reload Scene", ( EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableSceneReload ) == 0 );
            Menu.SetChecked( ITEM_NAME_ROOT + "Disable Scene Backup", ( EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableSceneBackupUnlessDirty ) == 0 );
        }

        /// <summary>
        /// Project Settings ウィンドウを再描画します
        /// </summary>
        private static void RepaintProjectSettingsWindow()
        {
            var projectSettingsWindow = Resources
                    .FindObjectsOfTypeAll<EditorWindow>()
                    .FirstOrDefault( x => x.GetType().ToString() == "UnityEditor.ProjectSettingsWindow" )
                ;

            if ( projectSettingsWindow == null ) return;

            projectSettingsWindow.Repaint();
        }
    }
}