using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_PlayerDetect : MonoBehaviour
{

    public PlayerController controller;
    public ParticleSystem eatParticles;
    public GameObject player;
    public GameObject gameManager;
    private bool inRange = false;
    private Vector3 nearestLatch;
    private Quaternion nearestLatchRotation;
    private GameObject currClothes;
    private bool isLatched = false;
    private RigidbodyConstraints latchConst = RigidbodyConstraints.FreezeAll;

    public GameObject sceneManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Clothes")
        {
            currClothes = other.gameObject;
            nearestLatch = other.transform.GetChild(0).transform.position;
            nearestLatchRotation = other.transform.GetChild(0).transform.rotation;
            Debug.Log(other.transform.GetChild(0).name);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Clothes")
        {
            currClothes = null;
            nearestLatch = Vector3.zero;
            inRange = false;
        }
    }

    public bool checkInRange()
    {
        return inRange;
    }
    public GameObject getCurrentClothes()
    {
        return currClothes;
    }

    //Latching
    public void toggleLatch(bool inRange)
    {
        //If in range of clothes and not latched
        if (inRange)
        {
            player.transform.rotation = nearestLatchRotation;
            player.transform.position = nearestLatch;
            isLatched = true;
            player.GetComponent<Rigidbody>().constraints = latchConst;
            
        }

    }

    public void eatClothes()
    {
        if (getCurrentClothes() == null)
        {
            return;
        }
        toggleLatch(checkInRange());
        controller.toggleControls();
        StartCoroutine(eatClothesProcess());
        StartCoroutine(playDamageEffect(eatParticles));
    }

    public IEnumerator eatClothesProcess()
    {
        yield return new WaitForSeconds(3.0f);
        // if we collide with food, increase hunger meter
        sceneManager.SendMessage("increaseHunger", 10f);

        // update game score based on the food's value
        sceneManager.SendMessage("updateScore", getCurrentClothes().GetComponent<FoodValue>().score);

        // TODO: apply any status effect on moth here (value stored on col.gameObject.GetComponent<FoodValue>().effect)
        gameManager.GetComponent<EndGameManager>().decreaseClothingAmount(getCurrentClothes());
        Destroy(getCurrentClothes());
        controller.toggleControls();
        player.GetComponent<Rigidbody>().constraints = controller.currConstr;
        player.transform.rotation = Quaternion.identity;
    }

    private IEnumerator playDamageEffect(ParticleSystem ps)
    {
        ParticleSystem particle = ps;
        ps.gameObject.SetActive(true);
        ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        emitOverride.startLifetime = 0.5f;
        ps.Emit(emitOverride, 20);
        yield return new WaitForSeconds(0.5f);
        ps.gameObject.SetActive(false);
    }
    
}
