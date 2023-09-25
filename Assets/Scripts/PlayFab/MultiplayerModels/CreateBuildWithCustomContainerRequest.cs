using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class CreateBuildWithCustomContainerRequest : PlayFabRequestCommon
	{
		public string BuildName;

		public ContainerFlavor? ContainerFlavor;

		public string ContainerRepositoryName;

		public string ContainerRunCommand;

		public string ContainerTag;

		public List<AssetReferenceParams> GameAssetReferences;

		public List<GameCertificateReferenceParams> GameCertificateReferences;

		public Dictionary<string, string> Metadata;

		public int MultiplayerServerCountPerVm;

		public List<Port> Ports;

		public List<BuildRegionParams> RegionConfigurations;

		public AzureVmSize? VmSize;
	}
}
