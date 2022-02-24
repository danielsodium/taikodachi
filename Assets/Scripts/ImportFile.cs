using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using System.IO.Compression;

public class ImportFile : MonoBehaviour
{

	public LoadMaps loader;
    private bool dialoged = false;
    private string mapsFolder;

	void Start()
	{
        mapsFolder = Path.Combine(Application.persistentDataPath, "maps");
        if (!Directory.Exists(mapsFolder)) {
		    Directory.CreateDirectory(mapsFolder);
        }
		
	}
    void Update() {
        if (Input.GetKeyDown(KeyCode.P) && !dialoged) {
            dialoged = true;
            StartCoroutine( ShowLoadDialogCoroutine() );
        }
    }

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load" );

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log( FileBrowser.Success );

		if( FileBrowser.Success )
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for( int i = 0; i < FileBrowser.Result.Length; i++ )
				Debug.Log( FileBrowser.Result[i] );

			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );

			// Or, copy the first file to persistentDataPath
            Debug.Log(Application.persistentDataPath);

            

			string destinationPath = Path.Combine( mapsFolder, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
			//FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
            if (!Directory.Exists(destinationPath)) {
                ZipFile.ExtractToDirectory(FileBrowser.Result[0],destinationPath);
            }
			loader.FindMaps();

            
		} else {
            dialoged = false;
        }
	}
}
