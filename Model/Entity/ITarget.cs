using Model.Entity.BulletPac;

namespace Model.Entity
{
    public interface ITarget
    {
        void GetBulletShot(Bullet bullet);
    }
}