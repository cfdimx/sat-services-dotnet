using SAT.Core.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.Core.DL.DAO.Relations
{
    public class RelationsDAO : DAOBase
    {
        public RelationsDAO(Database db) :base(db)
        {

        }

        private static Object _lock = new Object();
        private static RelationsDAO _instance = null;

        public static RelationsDAO Instance(Database db)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new RelationsDAO(db);
                    }
                }
            }

            return _instance;
        }

        public bool SaveRelations(Guid uuid, Guid parentUUID, string relationType)
        {
            try
            {
                Relation relation = new Relation()
                {
                    UUID = uuid,
                    ParentUUID = parentUUID,
                    RelationType = relationType
                };

                _db.SaveRelation(relation);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Relation> GetRelationsParents(Guid uuid)
        {
            try
            {
                return _db.GetRelationsParents(uuid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Relation> GetRelationsChildren(Guid uuid)
        {
            try
            {
                return _db.GetRelationsChildren(uuid);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void DeleteParentRelations(Guid uuid)
        {
            var relations = _db.GetRelationsChildren(uuid);
            
            foreach (var r in relations)
            {
                _db.DeleteRelations(r);
            }
        }
    }
}
