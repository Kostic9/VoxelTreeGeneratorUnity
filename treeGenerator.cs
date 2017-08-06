using UnityEngine;

public class treeGenerator : MonoBehaviour {

    /*
	Voxel tree generator script by Aleksandar Kostic
	HOW TO USE THE GENERATOR:	
	-assign the "block" prefab and the materials for the wood block and for the leaf block
	-call the "generateTree(Vector3 pos)" method, where "pos" is the world position of the block where the tree will spawn
    EXAMPLE:
    (calling the method from another script, which is attached to the same game object as this script)
    GetComponent<treeGenerator>().generateTree(new Vector3(5, 0, 10));
    In this case, the tree will spawn at (5, 0, 10)    
	*/

    //The block prefab. The tree will be generated out of this game object
    public GameObject block;
    //The material of the leaf block
    public Material leaf;
    //The material of the wood block
    public Material wood;

    //Minimum and maximum values of the tree dimensions
    int height_min = 10;
    int height_max = 15;
    int volume_min = 15;
    int volume_max = 20;

    //You can also use two different prefabs for the wood and for the leaves
    //In that case, find the lines of code where we instantiate the prefabs (they are marked) and
    //change the "block" prefab to your separate prefabs
    //If you do this, you also might want to assign the materials directly to the prefabs through unity
    //and remove all the code that assigns materials

    public void generateTree(Vector3 pos) {
        Vector3 lastPos = pos;
        int height = Random.Range(height_min, height_max);
        int volume = Random.Range(volume_min, volume_max);
        volume += volume % 2 == 0 ? 1 : 0;
        for (int i = 0; i < height; i++) {
            //This is where we instantiate the "block prefab and assign the wood material"
            GameObject blockGO = Instantiate(block);
            blockGO.GetComponent<MeshRenderer>().material = wood;
            blockGO.transform.position = new Vector3(pos.x, lastPos.y + 1f, pos.z);
            lastPos = blockGO.transform.position;
        }
        for (int i = 0; i < volume; i ++) {
            for (int j = 0; j < volume; j++) {
                for (int k = 0; k < volume; k++) {
                    if (leafSpawn(i, j, k, volume)) {
                        //This is where we instantiate the "block prefab and assign the leaf material"
                        GameObject blockGO = Instantiate(block);
                        blockGO.GetComponent<MeshRenderer>().material = leaf;
                        blockGO.transform.position = new Vector3(
                        lastPos.x - volume / 2 + i,
                        lastPos.y - volume / 2 + j,
                        lastPos.z - volume / 2 + k);
                    }                    
                }
            }
        }
    }
    bool leafSpawn(int x, int y, int z, int vol) {
        float a = Mathf.Abs(-vol / 2 + x);
        float b = Mathf.Abs(-vol / 2 + y);
        float c = Mathf.Abs(-vol / 2 + z);
        float distance = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2) + Mathf.Pow(c, 2));
        return (distance < vol/2 && Random.Range(0, vol) > distance);
    }
}
