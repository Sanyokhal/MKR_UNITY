using UnityEngine;

public class CalculateVector : MonoBehaviour
{
    void Start()
    {
        int[] vector_1 = {1,-3,7};
        int[] vector_2 = {2,8,-6};

        float result = 0f;

        for (int i = 0; i < vector_1.Length; i++) 
            {
            result += vector_1[i]*vector_2[i];
            }
        Debug.Log($"Результат = {result}");
    }
    void Update()
    {
        
    }
}
