﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.Utils;

public partial class PhoneStringValidationBehaviorStyle : Behavior<Entry>
{

    public static readonly BindableProperty AttachBehaviorProperty =
        BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(PhoneStringValidationBehaviorStyle), false, propertyChanged: OnAttachBehaviorChanged);

    public static bool GetAttachBehavior(BindableObject view)
    {
        return (bool)view.GetValue(AttachBehaviorProperty);
    }

    public static void SetAttachBehavior(BindableObject view, bool value)
    {
        view.SetValue(AttachBehaviorProperty, value);
    }

    static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
    {
        Entry entry = view as Entry;
        if (entry == null)
        {
            return;
        }

        bool attachBehavior = (bool)newValue;
        if (attachBehavior)
        {
            entry.Behaviors.Add(new PhoneStringValidationBehaviorStyle());
        }
        else
        {
            Behavior toRemove = entry.Behaviors.FirstOrDefault(b => b is PhoneStringValidationBehaviorStyle);
            if (toRemove != null)
            {
                entry.Behaviors.Remove(toRemove);
            }
        }
    }
    void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        if (args?.NewTextValue is null)
            return;

        ((Entry)sender).BackgroundColor = Globals.MyPhoneRegex().IsMatch(args.NewTextValue) ? Colors.LightGreen : Colors.LightPink;
    }

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnEntryTextChanged;
        base.OnDetachingFrom(entry);
    }

    
}
