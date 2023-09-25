using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetBuildResponse : PlayFabResultCommon
	{
		public string BuildId;

		public string BuildName;

		public string BuildStatus;

		public ContainerFlavor? ContainerFlavor;

		public string ContainerRunCommand;

		public DateTime? CreationTime;

		public ContainerImageReference CustomGameContainerImage;

		public List<AssetReference> GameAssetReferences;

		public List<GameCertificateReference> GameCertificateReferences;

		public Dictionary<string, string> Metadata;

		public int MultiplayerServerCountPerVm;

		public List<Port> Ports;

		public List<BuildRegion> RegionConfigurations;

		public string StartMultiplayerServerCommand;

		public AzureVmSize? VmSize;
	}
}
