﻿namespace SearchRecordsApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.listField = new System.Windows.Forms.ListBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listIndexOfRecord = new System.Windows.Forms.ListBox();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textConcreteRecord = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textRecordCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listField
            // 
            this.listField.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listField.FormattingEnabled = true;
            this.listField.ItemHeight = 17;
            this.listField.Location = new System.Drawing.Point(12, 30);
            this.listField.Name = "listField";
            this.listField.Size = new System.Drawing.Size(198, 395);
            this.listField.TabIndex = 0;
            this.listField.SelectedIndexChanged += new System.EventHandler(this.listField_SelectedIndexChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.LightSalmon;
            this.buttonSearch.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSearch.Location = new System.Drawing.Point(232, 56);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(202, 30);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textSearch
            // 
            this.textSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textSearch.Location = new System.Drawing.Point(232, 30);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(202, 23);
            this.textSearch.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Список доступных значений";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(232, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "слово встречается в записях";
            // 
            // listIndexOfRecord
            // 
            this.listIndexOfRecord.FormattingEnabled = true;
            this.listIndexOfRecord.Location = new System.Drawing.Point(232, 122);
            this.listIndexOfRecord.Name = "listIndexOfRecord";
            this.listIndexOfRecord.Size = new System.Drawing.Size(202, 225);
            this.listIndexOfRecord.TabIndex = 5;
            this.listIndexOfRecord.SelectedIndexChanged += new System.EventHandler(this.listIndexOfRecord_SelectedIndexChanged);
            // 
            // textOutput
            // 
            this.textOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textOutput.Location = new System.Drawing.Point(493, 30);
            this.textOutput.Name = "textOutput";
            this.textOutput.Size = new System.Drawing.Size(245, 23);
            this.textOutput.TabIndex = 6;
            // 
            // buttonOutput
            // 
            this.buttonOutput.BackColor = System.Drawing.Color.LightSalmon;
            this.buttonOutput.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOutput.Location = new System.Drawing.Point(493, 56);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(245, 30);
            this.buttonOutput.TabIndex = 7;
            this.buttonOutput.Text = "Вывод";
            this.buttonOutput.UseVisualStyleBackColor = false;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(493, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Введите индекс записи для вывода";
            // 
            // textConcreteRecord
            // 
            this.textConcreteRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textConcreteRecord.Location = new System.Drawing.Point(450, 99);
            this.textConcreteRecord.Multiline = true;
            this.textConcreteRecord.Name = "textConcreteRecord";
            this.textConcreteRecord.ReadOnly = true;
            this.textConcreteRecord.Size = new System.Drawing.Size(338, 326);
            this.textConcreteRecord.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(232, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Введите значение";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(232, 361);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "найдено совпадений";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textRecordCount
            // 
            this.textRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textRecordCount.Location = new System.Drawing.Point(232, 383);
            this.textRecordCount.Name = "textRecordCount";
            this.textRecordCount.ReadOnly = true;
            this.textRecordCount.Size = new System.Drawing.Size(202, 23);
            this.textRecordCount.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(232, 442);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(267, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Длительность последней операции";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textTime
            // 
            this.textTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textTime.Location = new System.Drawing.Point(232, 464);
            this.textTime.Name = "textTime";
            this.textTime.ReadOnly = true;
            this.textTime.Size = new System.Drawing.Size(267, 23);
            this.textTime.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 518);
            this.Controls.Add(this.textTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textRecordCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textConcreteRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.listIndexOfRecord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.listField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listField;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listIndexOfRecord;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textConcreteRecord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textRecordCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textTime;
    }
}

