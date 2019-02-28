namespace Assets.Hatch.Scripts.Utilities
{
    public static class AudioUtility
    {
        /// <summary>
        /// Used for dynamically setting the audio source volume during a coroutine
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="maxDistance"></param>
        /// <param name="minDistance"></param>
        /// <param name="minVolume"></param>
        /// <param name="maxVolume"></param>
        /// <returns>Float value for audio source volume</returns>
        public static float GetDynamicVolumeUsingDistance(float distance, float maxDistance, float minDistance, float minVolume, float maxVolume)
        {
            var slope = (minVolume - maxVolume) / (maxDistance - minDistance);
            var yIntercept = minVolume - slope * maxDistance;

            return slope * distance + yIntercept;
        }
    }
}
