/*  This file is part of Chummer5a.
 *
 *  Chummer5a is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Chummer5a is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Chummer5a.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  You can obtain the full source code for Chummer5a at
 *  https://github.com/chummer5a/chummer5a
 */

using System.Threading;

namespace Chummer
{
    public static class SemaphoreSlimExtensions
    {
        /// <summary>
        /// Version of SemaphoreSlim::Wait() that also processes application events if this is called on the UI thread
        /// </summary>
        public static void SafeWait(this SemaphoreSlim objSemaphoreSlim, bool blnForceDoEvents = false)
        {
            if (Utils.EverDoEvents)
            {
                while (!objSemaphoreSlim.Wait(Utils.DefaultSleepDuration))
                    Utils.DoEventsSafe(blnForceDoEvents);
            }
            else
                objSemaphoreSlim.Wait();
        }

        /// <summary>
        /// Version of SemaphoreSlim::Wait() that also processes application events if this is called on the UI thread
        /// </summary>
        public static void SafeWait(this SemaphoreSlim objSemaphoreSlim, CancellationToken token, bool blnForceDoEvents = false)
        {
            if (Utils.EverDoEvents)
            {
                while (!objSemaphoreSlim.Wait(Utils.DefaultSleepDuration, token))
                    Utils.DoEventsSafe(blnForceDoEvents);
            }
            else
                objSemaphoreSlim.Wait(token);
        }
    }
}
