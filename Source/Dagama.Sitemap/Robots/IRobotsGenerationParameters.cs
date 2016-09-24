namespace Dagama.Sitemap.Robots
{
    public interface IRobotsGenerationParameters
    {
        /// <summary>
        /// Whether or not save the Sitemap path to the robots file.
        /// </summary>
        /// <returns>True if the Sitemap path should be saved to the robots file, false otherwise.
        /// </returns>
        bool ShouldSaveToRobotsFile();

        /// <summary>
        /// The file location of the robots file, relative to the website root folder. The robots
        /// file is usually just located at "robot.txt".
        /// </summary>
        /// <returns>The path to the robots file.</returns>
        string GetFileLocation();
    }
}
