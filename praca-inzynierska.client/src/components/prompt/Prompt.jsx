import styles from "./Prompt.module.css";

import { IoIosArrowDroprightCircle } from "react-icons/io";

function Prompt({ query, setQuery, onChange, onSubmit }) {
    return (
        <form className={styles.prompt} onSubmit={onSubmit}>
      <input
              type="text"
              value={query}
              onChange={(e) => setQuery(e.target.value)}
        className={styles.insert}
        placeholder="Insert to modify your image"
      />
      <button className={styles.iconBtn}>
        <IoIosArrowDroprightCircle size={48} />
      </button>

       <div className={styles.chooseFileBox}>
              <label className={styles.label} htmlFor="img">Select your image</label>
              <input className={styles.input} onChange={onChange} type='file' id="img" accept="image/*" />
      </div>
    </form>
  );
}

export default Prompt;
