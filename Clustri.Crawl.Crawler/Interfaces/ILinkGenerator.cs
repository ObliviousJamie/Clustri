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
        /// <param name="prefix">A prefix to add to the generated url</param>
        /// <returns></returns>
        string TransformToFriendLink(string user, string prefix = null);

        /// <summary>
        /// Changes link to username
        /// </summary>
        /// <param name="link">Link containing userid</param>
        /// <returns></returns>
        string TransformToUser(Uri link);

        /// <summary>
        /// Changes link to username
        /// </summary>
        /// <param name="profileLink">A link to a users homepage</param>
        /// <returns></returns>
        string TransformToFriendLink(Uri profileLink);
    }
}
