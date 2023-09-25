using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class CreateBuildWithManagedContainerRequest : PlayFabRequestCommon
	{
		public string BuildName;

		public ContainerFlavor? ContainerFlavor;

		public List<AssetReferenceParams> GameAssetReferences;

		public List<GameCertificateReferenceParams> GameCertificateReferences;

		public Dictionary<string, string> Metadata;

		public int MultiplayerServerCountPerVm;

		public List<Port> Ports;

		public List<BuildRegionParams> RegionConfigurations;

		public string StartMultiplayerServerCommand;

		public AzureVmSize? VmSize;
	}
}
