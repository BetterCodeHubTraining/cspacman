using System;
using System.Collections.Generic;

using eu.sig.cspacman.board;

namespace eu.sig.cspacman.level
{
    public delegate void CollisionHandler(Unit collider, Unit collidee);

    public class CollisionInteractionMap : ICollisionMap
    {

        private readonly IDictionary<Type, IDictionary<Type, CollisionHandler>> handlers;

        public CollisionInteractionMap()
        {
            this.handlers = new Dictionary<Type, IDictionary<Type, CollisionHandler>>();
        }

        public void OnCollision(Type collider, Type collidee, CollisionHandler handler)
        {
            OnCollision(collider, collidee, true, handler);
        }

        public void OnCollision(Type collider, Type collidee,
            bool symetric, CollisionHandler handler)
        {
            AddHandler(collider, collidee, handler);
            if (symetric)
            {
                AddHandler(collidee, collider,
                    (c1, c2) => handler(c2, c1));
            }
        }

        private void AddHandler(Type collider, Type collidee, CollisionHandler handler)
        {
            if (!handlers.ContainsKey(collider))
            {
                handlers[collider] = new Dictionary<Type, CollisionHandler>();
            }
            IDictionary<Type, CollisionHandler> map = handlers[collider];
            map[collidee] = handler;
        }

        public void Collide(Unit collider, Unit collidee)
        {
            Type colliderKey = GetMostSpecificClass(handlers,
                collider.GetType());
            if (colliderKey == null)
            {
                return;
            }

            IDictionary<Type, CollisionHandler> map = handlers[colliderKey];
            Type collideeKey = GetMostSpecificClass(map,
                collidee.GetType());
            if (collideeKey == null)
            {
                return;
            }

            CollisionHandler collisionHandler = (CollisionHandler)map[collideeKey];
            if (collisionHandler == null)
            {
                return;
            }

            collisionHandler(collider, collidee);
        }

        private Type GetMostSpecificClass<T>(IDictionary<Type, T> map, Type key)
        {
            IList<Type> collideeInheritance = GetInheritance(key);
            foreach (Type pointer in collideeInheritance)
            {
                if (map.ContainsKey(pointer))
                {
                    return pointer;
                }
            }
            return null;
        }

        private IList<Type> GetInheritance(Type clazz)
        {
            IList<Type> found = new List<Type>();
            found.Add(clazz);

            int index = 0;
            while (found.Count > index)
            {
                Type current = found[index];
                Type superClass = current.GetType().BaseType;
                if (superClass != null && typeof(Unit).IsAssignableFrom(superClass))
                {
                    found.Add(superClass);
                }
                foreach (Type classInterface in current.GetInterfaces())
                {
                    if (typeof(Unit).IsAssignableFrom(classInterface))
                    {
                        found.Add(classInterface);
                    }
                }
                index++;
            }

            return found;
        }
    }
}
