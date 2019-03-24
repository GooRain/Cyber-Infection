using Photon.Pun;

namespace CyberInfection.Extension.Pool
{
    public interface IPoolContainer
    {
        PhotonView cachedPhotonView { get; }
        void RpcPush(IPoolable poolObject);
        IPoolable Pop();
    }
}