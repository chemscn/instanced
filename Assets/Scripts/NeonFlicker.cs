// AI CODE
using UnityEngine;

public class NeonFlicker : MonoBehaviour
{
    [Header("Glow Settings")]
    [SerializeField] private Color glowColor = new Color(0f, 1f, 1f, 1f); // Cyan neon
    [SerializeField] private float glowIntensity = 3f;

    [Header("Flicker Settings")]
    [SerializeField] private float flickerSpeed = 0.1f;
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float flickerChance = 0.3f; // Chance of flicker per check
    [SerializeField] private bool randomFlicker = true;

    [Header("Noise Settings")]
    [SerializeField] private bool useNoise = true;
    [SerializeField] private float noiseAmount = 0.15f; // How much noise affects intensity
    [SerializeField] private bool useStaticNoise = true; // TV static style vs smooth Perlin

    private Material glowMaterial;
    private SpriteRenderer spriteRenderer;
    private float targetIntensity;
    private float currentIntensity;
    private float flickerTimer;
    private float noiseOffset;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("NeonFlicker requires a SpriteRenderer component!");
            enabled = false;
            return;
        }

        // Create a material instance with the Sprites-Default shader
        glowMaterial = new Material(Shader.Find("Sprites/Default"));
        spriteRenderer.material = glowMaterial;

        // Set initial values
        currentIntensity = maxIntensity;
        targetIntensity = maxIntensity;
        noiseOffset = Random.Range(0f, 100f); // Random offset for unique noise pattern

        UpdateGlow(currentIntensity);
    }

    void Update()
    {
        flickerTimer += Time.deltaTime;

        if (flickerTimer >= flickerSpeed)
        {
            flickerTimer = 0f;

            if (randomFlicker)
            {
                // Random chance to flicker
                if (Random.value < flickerChance)
                {
                    targetIntensity = Random.Range(minIntensity, maxIntensity);
                }
                else
                {
                    targetIntensity = maxIntensity;
                }
            }
            else
            {
                // Perlin noise for smoother flicker
                float noise = Mathf.PerlinNoise(Time.time * 5f, 0f);
                targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
            }
        }

        // Smooth transition to target intensity
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * 10f);

        // Add continuous noise layer
        float finalIntensity = currentIntensity;
        if (useNoise)
        {
            float noiseVariation;

            if (useStaticNoise)
            {
                // Random static noise like TV static
                noiseVariation = (Random.value - 0.5f) * 2f * noiseAmount;
            }
            else
            {
                // Smooth Perlin noise
                float noise = Mathf.PerlinNoise(Time.time * 10f + noiseOffset, noiseOffset);
                noiseVariation = (noise - 0.5f) * 2f * noiseAmount;
            }

            finalIntensity = Mathf.Clamp(currentIntensity + noiseVariation, minIntensity, maxIntensity);
        }

        UpdateGlow(finalIntensity);
    }

    void UpdateGlow(float intensity)
    {
        if (glowMaterial != null)
        {
            // Apply color with intensity
            Color finalColor = glowColor * (glowIntensity * intensity);
            glowMaterial.SetColor("_Color", finalColor);

            // Optional: Add HDR emission if using URP/HDRP
            if (glowMaterial.HasProperty("_EmissionColor"))
            {
                glowMaterial.EnableKeyword("_EMISSION");
                glowMaterial.SetColor("_EmissionColor", finalColor);
            }
        }
    }

    void OnDestroy()
    {
        // Clean up material instance
        if (glowMaterial != null)
        {
            Destroy(glowMaterial);
        }
    }
}