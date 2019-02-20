using UnityEngine;
using UnityEngine.AI;

public class GenerateEnemy : MonoBehaviour
{
    private GameObject obj;

    public void PlaceEnemy(GameObject enemy,int number, int radius)
    {
      
        for (int i = 0; i < number + 1; i++)
        {   
            
            Vector3 pos = new Vector3(Random.Range(-radius,radius),0,Random.Range(-radius,radius));
            GameObject root = Instantiate(enemy, pos,Quaternion.identity);
            root.name = "Enemy" + "("+i+")";
            NavMeshAgent agent = root.GetComponent<NavMeshAgent>();


            //Вызывает DeadLock
            //if (!agent.isOnNavMesh)
            //{
            //    Destroy(root);
            //    i--;
            //}
            //else if (Vector3.Distance(root.transform.position, GameObject.FindGameObjectWithTag("Enemy").transform.position) < 2)
            //{
            //    Destroy(root);
            //    i--;
            //}
        }


    }
    

    public void RemoveEnemyes()
    {
        while (obj !=null)
        {
            obj = GameObject.FindGameObjectWithTag("Enemy");
            DestroyImmediate(obj);
            
        }
     
    }
    
}
