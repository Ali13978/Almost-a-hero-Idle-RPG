using System;

public interface IGooglePlayObbDownloader
{
	string PublicKey { get; set; }

	string GetExpansionFilePath();

	string GetMainOBBPath();

	string GetPatchOBBPath();

	bool isObbCompleted();

	float getObbProgress();

	void restartActivity();

	void FetchOBB();
}
