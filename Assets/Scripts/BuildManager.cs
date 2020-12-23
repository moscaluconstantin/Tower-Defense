using UnityEngine;

public class BuildManager : MonoBehaviour
{
     public static BuildManager instance;

     public NodeUI nodeUI;

     public GameObject buildEffect;
     public GameObject sellEffect;

     private TurretBlueprint turretToBuild;
     private Node selectedNode;

     private void Awake()
     {
          if (instance == null)
               instance = this;
          return;
     }

     public bool CanBuild { get { return turretToBuild != null; } }
     public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

     public void SelectNode(Node node)
     {
          if (selectedNode == node)
          {
               DeselectNode();
               return;
          }

          selectedNode = node;
          turretToBuild = null;

          nodeUI.SetTarget(node);
     }
     public void DeselectNode()
     {
          selectedNode = null;
          nodeUI.Hide();
     }
     public void SelectTurretToBuild(TurretBlueprint turret)
     {
          turretToBuild = turret;
          DeselectNode();
     }
     public TurretBlueprint GetTurretToBuild()
     {
          return turretToBuild;
     }
}
