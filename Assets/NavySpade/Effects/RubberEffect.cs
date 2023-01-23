
/* ******************************************************************************************************* *
 * RUBBER EFFECT                                                                                           *
 * You need to set the Vertex Colors of your 3d model, on your prefered 3d Modelling Tool                  *
 * by Rodrigo Pegorari - 2010 - [url]http://rodrigopegorari.net[/url]                                                 *
 * based on the Processing 'Chain' code example ([url]http://www.processing.org/learning/topics/chain.html[/url])     *
 * ******************************************************************************************************* */

using UnityEngine;
using System.Collections;

public class RubberEffect : MonoBehaviour
{
    public float bounceFactor = 20;
    public float wobbleFactor = 10;

    public float maxTranslation = 0.05f;
    public float maxRotationDegrees = 5;

    private Vector3 oldBoneWorldPosition;
    private Quaternion oldBoneWorldRotation;
    private Vector3 animatedBoneWorldPosition;
    private Quaternion animatedBoneWorldRotation;
    private Quaternion goalRotation;
    private Vector3 goalPosition;

    void Awake()
    {
        oldBoneWorldPosition = transform.position;
        oldBoneWorldRotation = transform.rotation;
    }

    void LateUpdate()
    {
        JiggleBonesUpdate();
    }

    void JiggleBonesUpdate()
    {
        //Mesh has just been animated
        animatedBoneWorldPosition = transform.position;
        animatedBoneWorldRotation = transform.rotation;
        goalPosition = Vector3.Slerp(oldBoneWorldPosition, transform.position, Time.deltaTime * bounceFactor);
        goalRotation = Quaternion.Slerp(oldBoneWorldRotation, transform.rotation, Time.deltaTime * wobbleFactor);

        transform.rotation = Quaternion.RotateTowards(animatedBoneWorldRotation, goalRotation, maxRotationDegrees);
        transform.position = Vector3.MoveTowards(animatedBoneWorldPosition, goalPosition, maxTranslation);

        oldBoneWorldPosition = transform.position;
        oldBoneWorldRotation = transform.rotation;
    }

}