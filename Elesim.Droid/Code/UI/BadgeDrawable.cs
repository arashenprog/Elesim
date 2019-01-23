using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Elesim.Droid.Code.UI
{

    public class Utils2
    {
        public static void setBadgeCount(Context context, LayerDrawable icon, int count)
        {

            BadgeDrawable badge;

            // Reuse drawable if possible
            Drawable reuse = icon.FindDrawableByLayerId(Resource.Id.ic_badge);
            if (reuse != null && reuse is BadgeDrawable) {
                badge = (BadgeDrawable)reuse;
            } else {
                badge = new BadgeDrawable(context);
            }

            badge.SetCount(count);
            icon.Mutate();
            icon.SetDrawableByLayerId(Resource.Id.ic_badge, badge);
        }


    }

    public class BadgeDrawable : Drawable
    {

        private Paint mBadgePaint;
        private Paint mTextPaint;
        private Rect mTxtRect = new Rect();

        private string mCount = "";
        private bool mWillDraw = false;

        public override int Opacity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BadgeDrawable(Context context)
        {
            //mTextSize = context.getResources().getDimension(R.dimen.badge_text_size);

            mBadgePaint = new Paint();
            mBadgePaint.Color = Android.Graphics.Color.Red;
            mBadgePaint.AntiAlias = true;
            mBadgePaint.SetStyle(Paint.Style.Fill);

            mTextPaint = new Paint();
            mTextPaint.Color = Android.Graphics.Color.White;
            mTextPaint.SetTypeface(Typeface.DefaultBold);
            mTextPaint.TextSize = 16f;
            mTextPaint.AntiAlias = true;
            mTextPaint.TextAlign = Paint.Align.Center;
        }


        /*
        Sets the count (i.e notifications) to display.
         */
        public void SetCount(int count)
        {
            mCount = count.ToString();

            // Only draw a badge if there are notifications.
            mWillDraw = count > 0;
            InvalidateSelf();
        }


  

        public override void Draw(Canvas canvas)
        {
            if (!mWillDraw)
            {
                return;
            }

            Rect bounds = Bounds;
            float width = bounds.Right - bounds.Left;
            float height = bounds.Bottom - bounds.Top;

            // Position the badge in the top-right quadrant of the icon.
            float radius = (float)(((Math.Min(width, height) / 2) - 1) / 1.8);
            float centerX = width - radius - 1;
            float centerY = radius + 1;

            // Draw badge circle.
            canvas.DrawCircle(centerX, centerY, radius, mBadgePaint);

            // Draw badge count text inside the circle.
            mTextPaint.GetTextBounds(mCount, 0, mCount.Length, mTxtRect);
            float textHeight = mTxtRect.Bottom - mTxtRect.Top;
            float textY = centerY + (textHeight / 2f);
            canvas.DrawText(mCount, centerX, textY, mTextPaint);
        }

        public override void SetAlpha(int alpha)
        {
            //
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
           //
        }
    }
}