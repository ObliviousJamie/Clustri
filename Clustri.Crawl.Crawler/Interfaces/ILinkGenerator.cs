using System;

namespace Clustri.Crawl.Crawler.Interfaces
{
    /// <summary>
    /// Handles transformation of a userID to a url to crawl
    /// Example John Doe to www.friendface.com/id/johndoe
    /// </summary>
    public interface ILinkGenerator
    {
        /// <summary>
        /// Changes username to link
        /// </summary>
        /// <param name="user">User to transform to link to user page</param>
        /// <returns></returns>
        string TransformUser(string user);

        /// <summary>
        /// Changes link to username
        /// </summary>
        /// <param name="link">Link containing userid</param>
        /// <returns></returns>
        string TransformLink(Uri link);
    }
}
