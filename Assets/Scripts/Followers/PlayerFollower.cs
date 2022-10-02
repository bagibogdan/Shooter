using Player;
using Zenject;

namespace Followers
{
    public class PlayerFollower : ObjectFollower
    {
        [Inject]
        public void Construct(PlayerMovement playerMovement)
        {
            _followedObjectTransform = playerMovement.gameObject.transform;
        }
    }
}