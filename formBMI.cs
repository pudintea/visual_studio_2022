// File: FormBMI.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KalkulatorBMI
{
    // TIPE ENUMERATION untuk kategori BMI
    public enum KategoriBMI
    {
        KekuranganBeratBadan,
        Normal,
        KelebihanBeratBadan,
        Obesitas1,
        Obesitas2
    }

    // TIPE ENUMERATION untuk jenis kelamin
    public enum JenisKelamin
    {
        Pria,
        Wanita
    }

    public partial class FormBMI : Form
    {
        // KONSTANTA - Batas kategori BMI (sesuai WHO)
        private const double BMI_UNDERWEIGHT = 18.5;
        private const double BMI_NORMAL = 25.0;
        private const double BMI_OVERWEIGHT = 30.0;
        private const double BMI_OBESE_1 = 35.0;
        private const string APP_TITLE = "Kalkulator BMI";
        private const string APP_VERSION = "v1.0";

        // VARIABEL untuk menyimpan data
        private double beratBadan;
        private double tinggiBadan;
        private double hasilBMI;
        private KategoriBMI kategoriHasil;
        private int hitunganTotal = 0;

        // COMMON CONTROLS
        private TextBox txtBerat;
        private TextBox txtTinggi;
        private TextBox txtUsia;
        private RadioButton rbPria;
        private RadioButton rbWanita;
        private ComboBox cmbAktivitas;
        private CheckBox chkRiwayatPenyakit;
        private Label lblHasil;
        private Label lblKategori;
        private Label lblKeterangan;
        private Label lblJumlahHitung;
        private ProgressBar progressBar;
        private ListView listRiwayat;

        // CONTAINER CONTROLS
        private Panel panelInput;
        private Panel panelHasil;
        private GroupBox grpJenisKelamin;
        private TabControl tabControl;
        private TabPage tabKalkulator;
        private TabPage tabRiwayat;
        private TabPage tabInfo;

        // BUTTON AND MENU
        private Button btnHitung;
        private Button btnReset;
        private Button btnSimpan;
        private Button btnHapusRiwayat;
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;

        public FormBMI()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = APP_TITLE + " " + APP_VERSION;
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // MENU STRIP - Menu Events
            CreateMenuStrip();

            // TOOL STRIP - Button Events
            CreateToolStrip();

            // TAB CONTROL - Container Control
            CreateTabControl();

            // Tab Kalkulator
            CreateTabKalkulator();

            // Tab Riwayat
            CreateTabRiwayat();

            // Tab Info
            CreateTabInfo();
        }

        // MENU STRIP dengan Menu Events
        private void CreateMenuStrip()
        {
            menuStrip = new MenuStrip();
            menuStrip.BackColor = Color.FromArgb(52, 73, 94);
            menuStrip.ForeColor = Color.White;

            // Menu File
            ToolStripMenuItem menuFile = new ToolStripMenuItem("File");
            menuFile.ForeColor = Color.White;
            ToolStripMenuItem menuNew = new ToolStripMenuItem("New Calculation", null, MenuNew_Click);
            menuNew.ShortcutKeys = Keys.Control | Keys.N;
            ToolStripMenuItem menuExit = new ToolStripMenuItem("Exit", null, MenuExit_Click);
            menuFile.DropDownItems.Add(menuNew);
            menuFile.DropDownItems.Add(new ToolStripSeparator());
            menuFile.DropDownItems.Add(menuExit);

            // Menu Tools
            ToolStripMenuItem menuTools = new ToolStripMenuItem("Tools");
            menuTools.ForeColor = Color.White;
            ToolStripMenuItem menuClear = new ToolStripMenuItem("Clear History", null, MenuClearHistory_Click);
            ToolStripMenuItem menuSettings = new ToolStripMenuItem("Settings", null, MenuSettings_Click);
            menuTools.DropDownItems.Add(menuClear);
            menuTools.DropDownItems.Add(menuSettings);

            // Menu Help
            ToolStripMenuItem menuHelp = new ToolStripMenuItem("Help");
            menuHelp.ForeColor = Color.White;
            ToolStripMenuItem menuPanduan = new ToolStripMenuItem("Panduan BMI", null, MenuPanduan_Click);
            ToolStripMenuItem menuAbout = new ToolStripMenuItem("About", null, MenuAbout_Click);
            menuHelp.DropDownItems.Add(menuPanduan);
            menuHelp.DropDownItems.Add(new ToolStripSeparator());
            menuHelp.DropDownItems.Add(menuAbout);

            menuStrip.Items.Add(menuFile);
            menuStrip.Items.Add(menuTools);
            menuStrip.Items.Add(menuHelp);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        // TOOL STRIP dengan Button Events
        private void CreateToolStrip()
        {
            toolStrip = new ToolStrip();
            toolStrip.BackColor = Color.FromArgb(189, 195, 199);
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;

            ToolStripButton btnNewCalc = new ToolStripButton("New", null, ToolNew_Click);
            ToolStripButton btnClearHist = new ToolStripButton("Clear History", null, ToolClearHistory_Click);
            ToolStripSeparator separator = new ToolStripSeparator();
            ToolStripButton btnAbout = new ToolStripButton("About", null, ToolAbout_Click);

            toolStrip.Items.Add(btnNewCalc);
            toolStrip.Items.Add(separator);
            toolStrip.Items.Add(btnClearHist);
            toolStrip.Items.Add(btnAbout);

            this.Controls.Add(toolStrip);
        }

        // TAB CONTROL - Container Control
        private void CreateTabControl()
        {
            tabControl = new TabControl();
            tabControl.Location = new Point(10, toolStrip.Height + menuStrip.Height + 5);
            tabControl.Size = new Size(660, 480);
            tabControl.Font = new Font("Segoe UI", 10);

            tabKalkulator = new TabPage("Kalkulator BMI");
            tabRiwayat = new TabPage("Riwayat");
            tabInfo = new TabPage("Informasi");

            tabControl.TabPages.Add(tabKalkulator);
            tabControl.TabPages.Add(tabRiwayat);
            tabControl.TabPages.Add(tabInfo);

            this.Controls.Add(tabControl);
        }

        // TAB KALKULATOR dengan semua komponen
        private void CreateTabKalkulator()
        {
            // PANEL INPUT - Container Control
            panelInput = new Panel();
            panelInput.BackColor = Color.White;
            panelInput.BorderStyle = BorderStyle.FixedSingle;
            panelInput.Location = new Point(15, 15);
            panelInput.Size = new Size(615, 280);
            tabKalkulator.Controls.Add(panelInput);

            // Label Judul
            Label lblJudul = new Label();
            lblJudul.Text = "MASUKKAN DATA ANDA";
            lblJudul.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(52, 73, 94);
            lblJudul.Location = new Point(15, 10);
            lblJudul.AutoSize = true;
            panelInput.Controls.Add(lblJudul);

            // Berat Badan
            Label lblBerat = new Label();
            lblBerat.Text = "Berat Badan (kg):";
            lblBerat.Location = new Point(15, 50);
            lblBerat.AutoSize = true;
            panelInput.Controls.Add(lblBerat);

            txtBerat = new TextBox();
            txtBerat.Location = new Point(150, 47);
            txtBerat.Size = new Size(150, 25);
            txtBerat.KeyPress += TxtAngka_KeyPress;
            panelInput.Controls.Add(txtBerat);

            // Tinggi Badan
            Label lblTinggi = new Label();
            lblTinggi.Text = "Tinggi Badan (cm):";
            lblTinggi.Location = new Point(15, 85);
            lblTinggi.AutoSize = true;
            panelInput.Controls.Add(lblTinggi);

            txtTinggi = new TextBox();
            txtTinggi.Location = new Point(150, 82);
            txtTinggi.Size = new Size(150, 25);
            txtTinggi.KeyPress += TxtAngka_KeyPress;
            panelInput.Controls.Add(txtTinggi);

            // Usia
            Label lblUsia = new Label();
            lblUsia.Text = "Usia (tahun):";
            lblUsia.Location = new Point(15, 120);
            lblUsia.AutoSize = true;
            panelInput.Controls.Add(lblUsia);

            txtUsia = new TextBox();
            txtUsia.Location = new Point(150, 117);
            txtUsia.Size = new Size(150, 25);
            txtUsia.KeyPress += TxtAngkaInteger_KeyPress;
            panelInput.Controls.Add(txtUsia);

            // GROUP BOX - Container Control untuk Radio Button
            grpJenisKelamin = new GroupBox();
            grpJenisKelamin.Text = "Jenis Kelamin";
            grpJenisKelamin.Location = new Point(330, 40);
            grpJenisKelamin.Size = new Size(260, 100);
            panelInput.Controls.Add(grpJenisKelamin);

            // RADIO BUTTON - Common Control
            rbPria = new RadioButton();
            rbPria.Text = "Pria";
            rbPria.Location = new Point(15, 25);
            rbPria.Checked = true;
            grpJenisKelamin.Controls.Add(rbPria);

            rbWanita = new RadioButton();
            rbWanita.Text = "Wanita";
            rbWanita.Location = new Point(15, 55);
            grpJenisKelamin.Controls.Add(rbWanita);

            // COMBO BOX - Common Control
            Label lblAktivitas = new Label();
            lblAktivitas.Text = "Tingkat Aktivitas:";
            lblAktivitas.Location = new Point(15, 155);
            lblAktivitas.AutoSize = true;
            panelInput.Controls.Add(lblAktivitas);

            cmbAktivitas = new ComboBox();
            cmbAktivitas.Location = new Point(150, 152);
            cmbAktivitas.Size = new Size(440, 25);
            cmbAktivitas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAktivitas.Items.AddRange(new object[] {
                "Tidak Aktif (jarang olahraga)",
                "Sedikit Aktif (olahraga 1-3 hari/minggu)",
                "Cukup Aktif (olahraga 3-5 hari/minggu)",
                "Sangat Aktif (olahraga 6-7 hari/minggu)",
                "Super Aktif (atlet profesional)"
            });
            cmbAktivitas.SelectedIndex = 1;
            panelInput.Controls.Add(cmbAktivitas);

            // CHECK BOX - Common Control
            chkRiwayatPenyakit = new CheckBox();
            chkRiwayatPenyakit.Text = "Memiliki riwayat penyakit kronis (diabetes, jantung, dll)";
            chkRiwayatPenyakit.Location = new Point(15, 190);
            chkRiwayatPenyakit.Size = new Size(400, 25);
            panelInput.Controls.Add(chkRiwayatPenyakit);

            // PROGRESS BAR - Common Control
            Label lblProgress = new Label();
            lblProgress.Text = "Status Kalkulasi:";
            lblProgress.Location = new Point(15, 210);
            lblProgress.AutoSize = true;
            panelInput.Controls.Add(lblProgress);

            progressBar = new ProgressBar();
            progressBar.Location = new Point(15, 230);
            progressBar.Size = new Size(575, 25);
            progressBar.Style = ProgressBarStyle.Continuous;
            panelInput.Controls.Add(progressBar);

            // BUTTON - Common Control dengan Button Events
            btnHitung = new Button();
            btnHitung.Text = "HITUNG BMI";
            btnHitung.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnHitung.BackColor = Color.FromArgb(46, 204, 113);
            btnHitung.ForeColor = Color.White;
            btnHitung.Size = new Size(150, 40);
            btnHitung.Location = new Point(15, 310);
            btnHitung.FlatStyle = FlatStyle.Flat;
            btnHitung.Cursor = Cursors.Hand;
            btnHitung.Click += BtnHitung_Click;
            tabKalkulator.Controls.Add(btnHitung);

            btnReset = new Button();
            btnReset.Text = "RESET";
            btnReset.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnReset.BackColor = Color.FromArgb(231, 76, 60);
            btnReset.ForeColor = Color.White;
            btnReset.Size = new Size(150, 40);
            btnReset.Location = new Point(180, 310);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Cursor = Cursors.Hand;
            btnReset.Click += BtnReset_Click;
            tabKalkulator.Controls.Add(btnReset);

            btnSimpan = new Button();
            btnSimpan.Text = "SIMPAN KE RIWAYAT";
            btnSimpan.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSimpan.BackColor = Color.FromArgb(52, 152, 219);
            btnSimpan.ForeColor = Color.White;
            btnSimpan.Size = new Size(180, 40);
            btnSimpan.Location = new Point(345, 310);
            btnSimpan.FlatStyle = FlatStyle.Flat;
            btnSimpan.Cursor = Cursors.Hand;
            btnSimpan.Enabled = false;
            btnSimpan.Click += BtnSimpan_Click;
            tabKalkulator.Controls.Add(btnSimpan);

            // Label Jumlah Perhitungan
            lblJumlahHitung = new Label();
            lblJumlahHitung.Text = "Total Perhitungan: 0";
            lblJumlahHitung.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblJumlahHitung.ForeColor = Color.Gray;
            lblJumlahHitung.Location = new Point(480, 360);
            lblJumlahHitung.AutoSize = true;
            tabKalkulator.Controls.Add(lblJumlahHitung);

            // PANEL HASIL - Container Control
            panelHasil = new Panel();
            panelHasil.BackColor = Color.White;
            panelHasil.BorderStyle = BorderStyle.FixedSingle;
            panelHasil.Location = new Point(15, 365);
            panelHasil.Size = new Size(615, 65);
            panelHasil.Visible = false;
            tabKalkulator.Controls.Add(panelHasil);

            lblHasil = new Label();
            lblHasil.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblHasil.ForeColor = Color.FromArgb(52, 152, 219);
            lblHasil.Location = new Point(15, 5);
            lblHasil.AutoSize = true;
            panelHasil.Controls.Add(lblHasil);

            lblKategori = new Label();
            lblKategori.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblKategori.Location = new Point(15, 35);
            lblKategori.AutoSize = true;
            panelHasil.Controls.Add(lblKategori);

            lblKeterangan = new Label();
            lblKeterangan.Font = new Font("Segoe UI", 9);
            lblKeterangan.ForeColor = Color.Gray;
            lblKeterangan.Location = new Point(250, 10);
            lblKeterangan.Size = new Size(350, 45);
            panelHasil.Controls.Add(lblKeterangan);
        }

        // TAB RIWAYAT - DIPERBAIKI
        private void CreateTabRiwayat()
        {
            // Label Header
            Label lblHeaderRiwayat = new Label();
            lblHeaderRiwayat.Text = "RIWAYAT PERHITUNGAN BMI";
            lblHeaderRiwayat.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblHeaderRiwayat.ForeColor = Color.FromArgb(52, 73, 94);
            lblHeaderRiwayat.Location = new Point(20, 15);
            lblHeaderRiwayat.AutoSize = true;
            tabRiwayat.Controls.Add(lblHeaderRiwayat);

            // ListView untuk menampilkan riwayat
            listRiwayat = new ListView();
            listRiwayat.View = View.Details;
            listRiwayat.FullRowSelect = true;
            listRiwayat.GridLines = true;
            listRiwayat.HideSelection = false;
            listRiwayat.Scrollable = true;
            listRiwayat.Location = new Point(20, 50);
            listRiwayat.Size = new Size(610, 330);
            listRiwayat.Font = new Font("Segoe UI", 9);

            // Tambahkan kolom
            listRiwayat.Columns.Add("Waktu", 140);
            listRiwayat.Columns.Add("Jenis Kelamin", 100);
            listRiwayat.Columns.Add("Berat (kg)", 80);
            listRiwayat.Columns.Add("Tinggi (cm)", 80);
            listRiwayat.Columns.Add("BMI", 70);
            listRiwayat.Columns.Add("Kategori", 130);

            tabRiwayat.Controls.Add(listRiwayat);

            // Tombol Hapus Riwayat
            btnHapusRiwayat = new Button();
            btnHapusRiwayat.Text = "Hapus Semua Riwayat";
            btnHapusRiwayat.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnHapusRiwayat.BackColor = Color.FromArgb(231, 76, 60);
            btnHapusRiwayat.ForeColor = Color.White;
            btnHapusRiwayat.Size = new Size(180, 40);
            btnHapusRiwayat.Location = new Point(20, 395);
            btnHapusRiwayat.FlatStyle = FlatStyle.Flat;
            btnHapusRiwayat.Cursor = Cursors.Hand;
            btnHapusRiwayat.Click += BtnHapusRiwayat_Click;
            tabRiwayat.Controls.Add(btnHapusRiwayat);

            // Label info
            Label lblInfoRiwayat = new Label();
            lblInfoRiwayat.Text = "Klik 'Simpan ke Riwayat' pada tab Kalkulator untuk menyimpan hasil perhitungan";
            lblInfoRiwayat.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblInfoRiwayat.ForeColor = Color.Gray;
            lblInfoRiwayat.Location = new Point(210, 405);
            lblInfoRiwayat.Size = new Size(420, 30);
            tabRiwayat.Controls.Add(lblInfoRiwayat);
        }

        // TAB INFO
        private void CreateTabInfo()
        {
            Label lblInfoJudul = new Label();
            lblInfoJudul.Text = "INFORMASI KATEGORI BMI";
            lblInfoJudul.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblInfoJudul.ForeColor = Color.FromArgb(52, 73, 94);
            lblInfoJudul.Location = new Point(20, 20);
            lblInfoJudul.AutoSize = true;
            tabInfo.Controls.Add(lblInfoJudul);

            string infoText = "Kategori BMI menurut WHO:\n\n" +
                              "• Kekurangan Berat Badan: BMI < 18.5\n" +
                              "  Risiko: Malnutrisi, osteoporosis, anemia\n\n" +
                              "• Normal (Ideal): BMI 18.5 - 24.9\n" +
                              "  Status: Berat badan ideal dan sehat\n\n" +
                              "• Kelebihan Berat Badan: BMI 25.0 - 29.9\n" +
                              "  Risiko: Pra-obesitas, mulai berisiko penyakit\n\n" +
                              "• Obesitas Tingkat 1: BMI 30.0 - 34.9\n" +
                              "  Risiko: Diabetes, hipertensi, penyakit jantung\n\n" +
                              "• Obesitas Tingkat 2: BMI ≥ 35.0\n" +
                              "  Risiko: Risiko sangat tinggi penyakit serius\n\n" +
                              "Catatan: BMI adalah panduan umum. Konsultasikan\n" +
                              "dengan dokter untuk penilaian kesehatan lengkap.";

            // Panel scrollable
            Panel pnlScrollable = new Panel();
            pnlScrollable.Location = new Point(20, 60);
            pnlScrollable.Size = new Size(600, 350);
            pnlScrollable.AutoScroll = true;
            pnlScrollable.BorderStyle = BorderStyle.FixedSingle;
            pnlScrollable.BackColor = Color.White;
            tabInfo.Controls.Add(pnlScrollable);

            // Label panjang ditempatkan di dalam panel scrollable
            Label lblInfo = new Label();
            lblInfo.Text = infoText;
            lblInfo.Font = new Font("Segoe UI", 10);
            lblInfo.Location = new Point(10, 10);
            lblInfo.AutoSize = true;
            lblInfo.MaximumSize = new Size(560, 0);
            pnlScrollable.Controls.Add(lblInfo);
        }

        // EVENT: KeyPress untuk input angka desimal
        private void TxtAngka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') && ((sender as TextBox).Text.IndexOfAny(new char[] { '.', ',' }) > -1))
            {
                e.Handled = true;
            }
        }

        // EVENT: KeyPress untuk input angka integer
        private void TxtAngkaInteger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // BUTTON EVENT: Hitung BMI dengan OPERATOR ARITMATIKA
        private void BtnHitung_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDASI dengan MESSAGE BOX
                if (string.IsNullOrWhiteSpace(txtBerat.Text) || string.IsNullOrWhiteSpace(txtTinggi.Text))
                {
                    MessageBox.Show("Mohon isi semua data yang diperlukan!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Progress bar animation
                progressBar.Value = 0;
                Timer timer = new Timer();
                timer.Interval = 10;
                timer.Tick += (s, ev) =>
                {
                    if (progressBar.Value < 100)
                        progressBar.Value += 2;
                    else
                        timer.Stop();
                };
                timer.Start();

                // VARIABEL lokal untuk perhitungan
                beratBadan = Convert.ToDouble(txtBerat.Text.Replace(',', '.'));
                tinggiBadan = Convert.ToDouble(txtTinggi.Text.Replace(',', '.'));

                // Validasi nilai
                if (beratBadan <= 0 || tinggiBadan <= 0)
                {
                    MessageBox.Show("Berat dan tinggi badan harus lebih dari 0!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // OPERATOR ARITMATIKA: Hitung BMI
                // Rumus: BMI = berat (kg) / (tinggi (m))^2
                double tinggiMeter = tinggiBadan / 100.0;  // Operator pembagian
                double tinggiKuadrat = tinggiMeter * tinggiMeter;  // Operator perkalian
                hasilBMI = beratBadan / tinggiKuadrat;  // Operator pembagian

                // Tentukan kategori menggunakan ENUMERATION
                kategoriHasil = TentukanKategori(hasilBMI);

                // Tampilkan hasil
                TampilkanHasil();

                // Increment jumlah perhitungan (OPERATOR ARITMATIKA: increment)
                hitunganTotal++;
                lblJumlahHitung.Text = "Total Perhitungan: " + hitunganTotal.ToString();

                // Enable tombol simpan
                btnSimpan.Enabled = true;

                // MESSAGE BOX - Peringatan jika ada riwayat penyakit
                if (chkRiwayatPenyakit.Checked)
                {
                    MessageBox.Show("Anda memiliki riwayat penyakit kronis. " +
                        "Sangat disarankan untuk berkonsultasi dengan dokter!",
                        "Perhatian Khusus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Format input tidak valid! Gunakan angka yang benar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fungsi untuk menentukan kategori BMI menggunakan ENUMERATION
        private KategoriBMI TentukanKategori(double bmi)
        {
            if (bmi < BMI_UNDERWEIGHT)
                return KategoriBMI.KekuranganBeratBadan;
            else if (bmi >= BMI_UNDERWEIGHT && bmi < BMI_NORMAL)
                return KategoriBMI.Normal;
            else if (bmi >= BMI_NORMAL && bmi < BMI_OVERWEIGHT)
                return KategoriBMI.KelebihanBeratBadan;
            else if (bmi >= BMI_OVERWEIGHT && bmi < BMI_OBESE_1)
                return KategoriBMI.Obesitas1;
            else
                return KategoriBMI.Obesitas2;
        }

        // Fungsi untuk menampilkan hasil
        private void TampilkanHasil()
        {
            lblHasil.Text = "BMI: " + hasilBMI.ToString("F2");

            string kategoriText;
            Color warnaKategori;
            string keterangan;

            // Switch berdasarkan ENUMERATION
            switch (kategoriHasil)
            {
                case KategoriBMI.KekuranganBeratBadan:
                    kategoriText = "Kekurangan Berat Badan";
                    warnaKategori = Color.FromArgb(241, 196, 15);
                    keterangan = "Tingkatkan asupan nutrisi.";
                    break;
                case KategoriBMI.Normal:
                    kategoriText = "Normal (Ideal)";
                    warnaKategori = Color.FromArgb(46, 204, 113);
                    keterangan = "Pertahankan pola hidup sehat!";
                    break;
                case KategoriBMI.KelebihanBeratBadan:
                    kategoriText = "Kelebihan Berat Badan";
                    warnaKategori = Color.FromArgb(230, 126, 34);
                    keterangan = "Mulai atur pola makan & olahraga.";
                    break;
                case KategoriBMI.Obesitas1:
                    kategoriText = "Obesitas Tingkat 1";
                    warnaKategori = Color.FromArgb(231, 76, 60);
                    keterangan = "Konsultasi dengan dokter.";
                    break;
                case KategoriBMI.Obesitas2:
                    kategoriText = "Obesitas Tingkat 2";
                    warnaKategori = Color.FromArgb(192, 57, 43);
                    keterangan = "Segera konsultasi dokter!";
                    break;
                default:
                    kategoriText = "Unknown";
                    warnaKategori = Color.Gray;
                    keterangan = "";
                    break;
            }

            lblKategori.Text = "Kategori: " + kategoriText;
            lblKategori.ForeColor = warnaKategori;
            lblKeterangan.Text = keterangan;

            panelHasil.Visible = true;
        }

        // BUTTON EVENT: Reset
        private void BtnReset_Click(object sender, EventArgs e)
        {
            // MESSAGE BOX konfirmasi
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin mereset semua data?",
                "Konfirmasi Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                txtBerat.Clear();
                txtTinggi.Clear();
                txtUsia.Clear();
                rbPria.Checked = true;
                cmbAktivitas.SelectedIndex = 1;
                chkRiwayatPenyakit.Checked = false;
                panelHasil.Visible = false;
                progressBar.Value = 0;
                btnSimpan.Enabled = false;
            }
        }

        // BUTTON EVENT: Simpan ke Riwayat
        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            string jenisKelamin = rbPria.Checked ? "Pria" : "Wanita";
            string waktu = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string kategoriText = GetKategoriText(kategoriHasil);

            // Tambahkan item baru ke ListView
            ListViewItem item = new ListViewItem(waktu);
            item.SubItems.Add(jenisKelamin);
            item.SubItems.Add(beratBadan.ToString("0.##"));
            item.SubItems.Add(tinggiBadan.ToString("0.##"));
            item.SubItems.Add(hasilBMI.ToString("0.00"));
            item.SubItems.Add(kategoriText);

            listRiwayat.Items.Add(item);

            // Auto scroll ke item terakhir
            if (listRiwayat.Items.Count > 0)
            {
                listRiwayat.Items[listRiwayat.Items.Count - 1].EnsureVisible();
            }

            MessageBox.Show("Data berhasil disimpan ke riwayat!", "Sukses",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Pindah otomatis ke tab riwayat
            tabControl.SelectedTab = tabRiwayat;
        }

        // Helper method untuk mendapatkan teks kategori
        private string GetKategoriText(KategoriBMI kategori)
        {
            switch (kategori)
            {
                case KategoriBMI.KekuranganBeratBadan:
                    return "Kekurangan Berat Badan";
                case KategoriBMI.Normal:
                    return "Normal (Ideal)";
                case KategoriBMI.KelebihanBeratBadan:
                    return "Kelebihan Berat Badan";
                case KategoriBMI.Obesitas1:
                    return "Obesitas Tingkat 1";
                case KategoriBMI.Obesitas2:
                    return "Obesitas Tingkat 2";
                default:
                    return "Unknown";
            }
        }

        // BUTTON EVENT: Hapus semua riwayat
        private void BtnHapusRiwayat_Click(object sender, EventArgs e)
        {
            if (listRiwayat.Items.Count == 0)
            {
                MessageBox.Show("Tidak ada riwayat untuk dihapus.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult res = MessageBox.Show("Anda yakin ingin menghapus semua riwayat?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                listRiwayat.Items.Clear();
                MessageBox.Show("Semua riwayat berhasil dihapus.", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // MENU EVENT HANDLERS
        private void MenuNew_Click(object sender, EventArgs e)
        {
            txtBerat.Clear();
            txtTinggi.Clear();
            txtUsia.Clear();
            rbPria.Checked = true;
            cmbAktivitas.SelectedIndex = 1;
            chkRiwayatPenyakit.Checked = false;
            panelHasil.Visible = false;
            progressBar.Value = 0;
            btnSimpan.Enabled = false;
            tabControl.SelectedTab = tabKalkulator;
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Keluar dari aplikasi?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
                Application.Exit();
        }

        private void MenuClearHistory_Click(object sender, EventArgs e)
        {
            if (listRiwayat.Items.Count > 0)
            {
                DialogResult r = MessageBox.Show("Hapus semua riwayat?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    listRiwayat.Items.Clear();
                    MessageBox.Show("Riwayat dihapus.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Tidak ada riwayat untuk dihapus.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pengaturan belum tersedia pada versi ini.", "Settings",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kalkulator BMI\nVersi 1.0\n\nDibuat menggunakan Windows Forms (C#)\n\n" +
                "Fitur:\n• Perhitungan BMI\n• Kategori kesehatan\n• Riwayat perhitungan\n• Menu dan toolbar",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuPanduan_Click(object sender, EventArgs e)
        {
            string panduan = "=== PANDUAN PENGGUNAAN ===\n\n" +
                "1. Masukkan berat badan (kg) dan tinggi badan (cm)\n" +
                "2. Pilih jenis kelamin dan tingkat aktivitas\n" +
                "3. Klik tombol 'HITUNG BMI'\n" +
                "4. Hasil akan ditampilkan beserta kategorinya\n" +
                "5. Klik 'SIMPAN KE RIWAYAT' untuk menyimpan\n" +
                "6. Lihat riwayat di tab 'Riwayat'\n\n" +
                "Tips: Gunakan tombol reset untuk membersihkan form";
            MessageBox.Show(panduan, "Panduan BMI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // TOOL STRIP EVENT HANDLERS
        private void ToolNew_Click(object sender, EventArgs e)
        {
            MenuNew_Click(sender, e);
        }

        private void ToolClearHistory_Click(object sender, EventArgs e)
        {
            MenuClearHistory_Click(sender, e);
        }

        private void ToolAbout_Click(object sender, EventArgs e)
        {
            MenuAbout_Click(sender, e);
        }
    }
}
